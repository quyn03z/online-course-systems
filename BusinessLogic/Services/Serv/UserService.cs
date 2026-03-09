using Azure;
using BusinessLogic.Claims;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.PageResultModel;
using DataAccess.Repositories.Impl;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static BusinessLogic.Models.User;

namespace BusinessLogic.Services.Serv
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		private readonly IResetPasswordTokenRepository _resetPasswordTokenRepository;
		private readonly IEmailService _emailService;
		private readonly IConfiguration _configuration;
		private readonly IClaimService _claimService;
		private readonly IAuditLogsService _auditLogsService;

		public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IRefreshTokenRepository refreshTokenRepository, IResetPasswordTokenRepository resetPasswordTokenRepository, IEmailService emailService, IConfiguration configuration, IClaimService claimService, IAuditLogsService auditLogsService)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_refreshTokenRepository = refreshTokenRepository;
			_resetPasswordTokenRepository = resetPasswordTokenRepository;
			_emailService = emailService;
			_configuration = configuration;
			_claimService = claimService;
			_auditLogsService = auditLogsService;
		}

		public async Task<CreateUserResponseModel> CreateUserAsync(CreateUserModel createUserModel)
		{
			// ktra email tồn tại
			if (await _userRepository.GetUserByEmail(createUserModel.Email) != null)
				throw new BadRequestException("Email đã tồn tại trong hệ thống!");

			// ktra username tồn tại 
			if (await _userRepository.GetUserByUserNameAsync(createUserModel.UserName) != null)
				throw new BadRequestException("Username đã tồn tại trong hệ thống!");

			// lấy role
			var role = await _roleRepository.GetRoleNameAsync("Student");

			// tạo user mới
			var user = new Domain.Models.User
			{
				RoleId = role.Id,
				Username = createUserModel.UserName,
				Email = createUserModel.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(createUserModel.Password),
				Avatar = "https://t4.ftcdn.net/jpg/07/03/86/11/360_F_703861114_7YxIPnoH8NfmbyEffOziaXy0EO1NpRHD.jpg"
			};

			// lưu user
			var createdUser = await _userRepository.AddAsync(user);

			return new CreateUserResponseModel
			{
				Id = createdUser.UserId,
			};
		}

		public async Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel)
		{
			var user = await _userRepository.GetUserByUserNameAsync(loginUserModel.Username);
			if (user == null)
				throw new BadRequestException("Tên đăng nhập không chính xác.");

			if (!BCrypt.Net.BCrypt.Verify(loginUserModel.Password, user.Password))
			{
				throw new BadRequestException("Mật khẩu nhập không chính xác.");
			}

			if (user.IsLocked)
			{
				throw new BadRequestException("Tài khoản đã bị khóa.");
			}

			var accessToken = JwtHelper.GenerateToken(user, _configuration);

			await _refreshTokenRepository.RevokeUserTokensAsync(user.UserId);

			var refeshToken = JwtHelper.GenerateRefreshToken();

			var tokenRefresh = new RefreshToken
			{
				UserId = user.UserId,
				Token = refeshToken,
				IsRevoked = false,
				ExpiredAt = DateTime.Now.AddDays(7),
				CreatedAt = DateTime.Now,
			};

			await _refreshTokenRepository.AddAsync(tokenRefresh);

			await _auditLogsService.LogActionAsync(user.UserId, "Login", "User", keyValues: $"{{ \"UserId\": {user.UserId} }}");

			return new LoginResponseModel
			{
				Role = user.Role.RoleName,
				Token = accessToken,
				RefreshToken = refeshToken,
			};
		}

		public async Task LogoutAsync()
		{
			var userId = _claimService.GetUserId();
			if (userId == null)
				throw new BusinessLogic.Exceptions.UnauthorizedException("Người dùng chưa xác thực.");
			await _refreshTokenRepository.RevokeUserTokensAsync(userId.Value);
			await _auditLogsService.LogActionAsync(userId.Value, "Logout", "User", keyValues: $"{{ \"UserId\": {userId.Value} }}");
		}

		public async Task<ForgotPassWordModel> ForgotPasswordAsync(EmailRequest email)
		{
			var user = await _userRepository.GetUserByEmail(email.Email);
			if (user == null)
				throw new BadRequestException("Email không tồn tại.");
			
			var resetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
			var resetTokenExpiry = DateTime.Now.AddMinutes(2);

			await _resetPasswordTokenRepository.RevokeResetTokensAsync(user.UserId);

			await _resetPasswordTokenRepository.AddAsync(new ResetPasswordToken
			{
				UserId = user.UserId,
				ResetToken = resetToken,
				ExpiredAt = resetTokenExpiry,
				isUsed = false,
				CreateAt = DateTime.Now,
			});
			string safeToken = HttpUtility.UrlEncode(resetToken);

			var resetLink = $"{_configuration["AppUrl"]}/reset-password?token={safeToken}";

			// Gửi email reset password
			await _emailService.SendEmailResetPasswordAsync(new ForgotPasswordModel
			{
				Email = email.Email,
				ResetLink = resetLink
			});
			return new ForgotPassWordModel
			{
				Email = email.Email,
				Token = resetToken,
			};
		}

		public async Task<ResetPasswordModel> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
		{
			var userResetPassToken = await _resetPasswordTokenRepository.GetByTokenAsync(resetPasswordModel.Token);

			if (userResetPassToken == null)
				throw new BadRequestException("Token không hợp lệ hoặc đã được sử dụng.");

			if (DateTime.Now > userResetPassToken.ExpiredAt)
				throw new BadRequestException("Hết thời gian thay đổi mật khẩu.");

			// Get user from token and update password
			var user = userResetPassToken.User;
			user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordModel.Password);
			await _userRepository.UpdateAsync(user);

			// Mark token as used
			userResetPassToken.isUsed = true;
			await _resetPasswordTokenRepository.UpdateAsync(userResetPassToken);

			return new ResetPasswordModel
			{
				Token = resetPasswordModel.Token,
				Password = resetPasswordModel.Password,
				ConfirmPassword = resetPasswordModel.ConfirmPassword,
			};

		}

		public async Task<PagedResults<UserResponseModel>> GetAllUserAdminPagedAsync(int page, int pageSize,string? search)
		{
			var pagedUsers = await _userRepository.GetAllUserAdminPagedAsync(page, pageSize,search);
			return pagedUsers.Map(u => new UserResponseModel
			{
				Id = u.UserId,
				Username = u.Username.Trim(),
				Firstname = u.Firstname?.Trim(),
				Lastname = u.Lastname?.Trim(),
				Email = u.Email.Trim(),
				IsLocked = u.IsLocked,
				RoleName = u.Role?.RoleName.Trim()
			});
		}

		public async Task<UserResponseModel> AddUserByAdmin(AddUserAdminModel addUserAdminModel)
		{
			if (await _userRepository.ExistsByEmailAsync(addUserAdminModel.Email))
				throw new BadRequestException("Email đã tồn tại trong hệ thống!");

			if(await _userRepository.ExistsByUserNameAsync(addUserAdminModel.UserName))
				throw new BadRequestException("Username đã tồn tại trong hệ thống!");

			var randomPassword = PasswordGenerator.GeneratePassword(8);

			var user = new Domain.Models.User
			{
				RoleId = addUserAdminModel.RoleId,
				Username = addUserAdminModel.UserName,
				Email = addUserAdminModel.Email,
				Firstname = addUserAdminModel.FirstName,
				Lastname = addUserAdminModel.LastName,
				IsLocked = addUserAdminModel.IsLocked,
				Password = BCrypt.Net.BCrypt.HashPassword(randomPassword)
			};

			await _userRepository.AddAsync(user);

			// Gửi email chào mừng kèm mật khẩu tạm thời
			await _emailService.SendWelcomeEmailAsync(user.Email, user.Username, randomPassword);

			return new UserResponseModel {
				Id = user.UserId,
				Username = user.Username,
				Firstname = user.Firstname,
				Lastname = user.Lastname,
				Email = user.Email,
				IsLocked = user.IsLocked,
				RoleName = user.Role?.RoleName
			};
		}

		public async Task<string> BlockUserAdmin(int targetId)
		{
			var currentId = _claimService.GetUserId();
			if (currentId == targetId)
				throw new BadRequestException("Bạn không thể khóa tài khoản của mình.");

			var user = await _userRepository.GetByIdAsync(targetId);
			if (user == null)
				throw new NotFoundException("Không tìm thấy người dùng.");

			user.IsLocked = true;
			await _userRepository.UpdateAsync(user);
			return $"Khóa tài khoản '{user.Username.Trim()}' thành công.";
		}

		public async Task<UserResponseModel> EditUserAdmin(UserRequest userRequest)
		{
			// Lấy user hiện tại từ DB
			var user = await _userRepository.GetByIdAsync(userRequest.UserId);
			if (user == null)
				throw new NotFoundException("Không tìm thấy người dùng.");

			// Kiểm tra email trùng — loại trừ chính user đang sửa
			if (user.Email != userRequest.Email && await _userRepository.ExistsByEmailAsync(userRequest.Email))
				throw new BadRequestException("Email đã tồn tại trong hệ thống!");

			// Cập nhật các field
			user.RoleId = userRequest.RoleId;
			user.Email = userRequest.Email;
			user.Firstname = userRequest.FirstName;
			user.Lastname = userRequest.LastName;
			user.IsLocked = userRequest.IsLocked;

			await _userRepository.UpdateAsync(user);

			return new UserResponseModel
			{
				Id = user.UserId,
				Username = user.Username,
				Firstname = user.Firstname,
				Lastname = user.Lastname,
				Email = user.Email,
				IsLocked = user.IsLocked,
				RoleName = user.Role?.RoleName
			};
		}

		public async Task<LoginResponseModel> RefreshTokenAsync(TokenRequestModel tokenRequestModel)
		{
			// 1. Tìm refresh token trong DB (chưa revoke + chưa hết hạn)
			var storedToken = await _refreshTokenRepository.GetByTokenAsync(tokenRequestModel.RefreshToken);
			if (storedToken == null)
				throw new BadRequestException("Refresh token không hợp lệ hoặc đã hết hạn.");

			var user = storedToken.User;

			if (user.IsLocked)
				throw new BadRequestException("Tài khoản đã bị khóa.");

			// 2. Thu hồi toàn bộ token cũ của user
			await _refreshTokenRepository.RevokeUserTokensAsync(user.UserId);

			// 3. Tạo access token + refresh token mới
			var newAccessToken = JwtHelper.GenerateToken(user, _configuration);
			var newRefreshToken = JwtHelper.GenerateRefreshToken();

			await _refreshTokenRepository.AddAsync(new RefreshToken
			{
				UserId = user.UserId,
				Token = newRefreshToken,
				IsRevoked = false,
				ExpiredAt = DateTime.Now.AddDays(7),
				CreatedAt = DateTime.Now,
			});

			return new LoginResponseModel
			{
				Role = user.Role.RoleName,
				Token = newAccessToken,
				RefreshToken = newRefreshToken,
			};
		}

		public async Task<string> ChangePasswordAsync(ChangePassWordModel changePassWordModel)
		{
			var userId = _claimService.GetUserId();
			if (userId == null)
				throw new UnauthorizedException("Người dùng chưa xác thực.");
			var user = await _userRepository.GetByIdAsync(userId.Value);
			
			if (!BCrypt.Net.BCrypt.Verify(changePassWordModel.OldPassword,user.Password))
				throw new BadRequestException("Mật khẩu cũ không chính xác");

			user.Password = BCrypt.Net.BCrypt.HashPassword(changePassWordModel.NewPassword);

			await _userRepository.UpdateAsync(user);

			return "Change Password Thành Công.";
		}

		public async Task<UserResponseProfile> GetUserByIdAsync()
		{
			var userId = _claimService.GetUserId();
			if (userId == null)
				throw new UnauthorizedException("Người dùng chưa xác thực.");
			var user = await _userRepository.GetUserByIdAsync(userId.Value);
			return new UserResponseProfile
			{
				Username = user.Username,
				LastName = user.Lastname,
				FirstName = user.Firstname,
				Email = user.Email,
				Avatar = user.Avatar,
				RoleName = user.Role.RoleName,
			};
		}

		public async Task<UserResponseProfile> UpdateProfileAsync(UpdateProfileRequestModel updateProfileRequestModel)
		{
			var userId = _claimService.GetUserId();
			if (userId == null)
				throw new UnauthorizedException("Người dùng chưa xác thực.");
			var user = await _userRepository.GetUserByIdAsync(userId.Value);
			user.Firstname = updateProfileRequestModel.FirstName;


			user.Lastname = updateProfileRequestModel.LastName;
				

			user.Avatar = !string.IsNullOrWhiteSpace(updateProfileRequestModel.Avatar)
				? updateProfileRequestModel.Avatar
				: user.Avatar;

			await _userRepository.UpdateAsync(user);
			return new UserResponseProfile
			{
				Username = user.Username,
				LastName = user.Lastname,
				FirstName = user.Firstname,
				Email = user.Email,
				Avatar = user.Avatar,
				RoleName = user.Role.RoleName,
			};
		}


	}
}

