using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.Enrollment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
	public class UserController : BaseController
	{
		private readonly IUserService _userService;
		private readonly IEnrollmentService _enrollmentService;

		public UserController(IUserService userService, IEnrollmentService enrollmentService)
		{
			_userService = userService;
			_enrollmentService = enrollmentService;
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

		[Authorize]
		[HttpGet("purchase-history")]
		public async Task<IActionResult> PurchaseHistoryByUserIdAsync()
		{
			return Ok(ApiResult<List<PurchaseHistoryModel>>.Success(await _enrollmentService.PurchaseHistoryByUserIdAsync()));
		}




	}
}
