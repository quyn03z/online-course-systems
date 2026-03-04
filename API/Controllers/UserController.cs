using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.QuizzModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
	public class UserController : BaseController
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}


		[Authorize]
		[HttpPut("change-password")]
		public async Task<IActionResult> ChangePasswordAsync(ChangePassWordModel changePassWordModel)
		{
			if (!ModelState.IsValid)
			return ValidationError();
			return Ok(ApiResult<string>.Success(await _userService.ChangePasswordAsync(changePassWordModel)));
		}

		[Authorize]
		[HttpGet("user-profile")]
		public async Task<IActionResult> GetUserByIdAsync()
		{
			return Ok(ApiResult<UserResponseProfile>.Success(await _userService.GetUserByIdAsync()));
		}

		[Authorize]
		[HttpPut("update-profile")]
		public async Task<IActionResult> UpdateProfileAsync(UpdateProfileRequestModel updateProfileRequestModel)
		{
			if (!ModelState.IsValid)
				return ValidationError();
			return Ok(ApiResult<UserResponseProfile>.Success(await _userService.UpdateProfileAsync(updateProfileRequestModel)));
		}

	}
}
