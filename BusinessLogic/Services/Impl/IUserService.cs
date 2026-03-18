using BusinessLogic.Models;
using DataAccess.Models.PageResultModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.Models.User;

namespace BusinessLogic.Services.Impl
{
	public interface IUserService
	{
		Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
		Task<CreateUserResponseModel> CreateUserAsync(CreateUserModel createUserModel);
		Task LogoutAsync();
		Task<ForgotPassWordModel> ForgotPasswordAsync(EmailRequest email);

		Task<ResetPasswordModel> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);

		Task<PagedResults<UserResponseModel>> GetAllUserAdminPagedAsync(int page, int pageSize, string? search = null);

		Task<UserResponseModel> AddUserByAdmin(AddUserAdminModel addUserAdminModel);

		Task<string> BlockUserAdmin(int targetId);

		Task<UserResponseModel> EditUserAdmin(UserRequest userRequest);

		Task<LoginResponseModel> RefreshTokenAsync(TokenRequestModel tokenRequestModel);

		Task<string> ChangePasswordAsync(ChangePassWordModel changePassWordModel);

		Task<UserResponseProfile> GetUserByIdAsync();

		Task<UserResponseProfile> UpdateProfileAsync(UpdateProfileRequestModel updateProfileRequestModel);




	}
}
