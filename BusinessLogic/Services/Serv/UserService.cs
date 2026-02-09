using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Services.Impl;
using DataAccess.Repositories.Impl;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.Models.User;

namespace BusinessLogic.Services.Serv
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		private readonly IConfiguration _configuration;

		public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_refreshTokenRepository = refreshTokenRepository;
			_configuration = configuration;
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
			var user = new User
			{
				RoleId = role.Id,
				Username = createUserModel.UserName,
				Email = createUserModel.Email,
				Firstname = createUserModel.FirstName,
				Lastname = createUserModel.LastName,
				Password = BCrypt.Net.BCrypt.HashPassword(createUserModel.Password)
			};

			// lưu user
			var createdUser = await _userRepository.AddAsync(user);

			return new CreateUserResponseModel
			{
				Id = createdUser.Id,
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

			await _refreshTokenRepository.RevokeUserTokensAsync(user.Id);

			var refeshToken = JwtHelper.GenerateRefreshToken();

			var tokenRefresh = new RefreshToken
			{
				UserId = user.Id,
				Token = refeshToken,
				IsRevoked = false,
				ExpiredAt = DateTime.Now.AddDays(7),
				CreatedAt = DateTime.Now,
			};

			await _refreshTokenRepository.AddAsync(tokenRefresh);

			return new LoginResponseModel
			{
				Username = user.Username,
				Email = user.Email,
				Role = user.Role.RoleName,
				Token = accessToken,
				RefreshToken = refeshToken,
			};
		}

		public async Task LogoutAsync(int userId)
		{
			await _refreshTokenRepository.RevokeUserTokensAsync(userId);
		}


	}
}

