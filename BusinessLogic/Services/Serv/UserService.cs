using BusinessLogic.Exceptions;
using BusinessLogic.Services.Impl;
using DataAccess.Repositories.Impl;
using Domain.Models;
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

		public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
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
				Usename = createUserModel.UserName,
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

		//public async Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel)
		//{
		//	var user = await _userRepository.GetUserByUserNameAsync(loginUserModel.Username);
		//	if (user == null)
		//		throw new NotFoundException("Tên đăng nhập không chính xác");

		//	if (!BCrypt.Net.BCrypt.Verify(loginUserModel.Password, user.Password))
		//	{
		//		throw new NotFoundException("Mật khẩu nhập không chính xác");
		//	}

		//	if (!user.IsLocked)
		//	{

		//	}


		//}

	}
}

