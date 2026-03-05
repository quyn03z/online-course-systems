using Azure;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.PageResultModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
	public class AdminController : BaseController
	{
		private readonly IUserService _userService;

		public AdminController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("get-all-users")]
		public async Task<IActionResult> GetAllUserAdminPagedAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
		{
			var result = await _userService.GetAllUserAdminPagedAsync(page, pageSize, search);
			return Ok(ApiResult<PagedResults<UserResponseModel>>.Success(result));
		}

		[HttpPost("create-user-admin")]
		public async Task<IActionResult> AddUserByAdmin(AddUserAdminModel addUserAdminModel)
		{
			if (!ModelState.IsValid)
				return ValidationError();
			return Ok(ApiResult<UserResponseModel>.Success(await _userService.AddUserByAdmin(addUserAdminModel)));
		}

		[HttpPut("block-user-admin")]
		public async Task<IActionResult> BlockUserAdmin(int targetId)
		{
			return Ok(ApiResult<string>.Success(await _userService.BlockUserAdmin(targetId)));
		}

		[HttpPut("edit-user-admin")]
		public async Task<IActionResult> EditUserAdmin(UserRequest userRequest)
		{
			if (!ModelState.IsValid)
				return ValidationError();
			return Ok(ApiResult<UserResponseModel>.Success(await _userService.EditUserAdmin(userRequest)));
		}


	}
}
