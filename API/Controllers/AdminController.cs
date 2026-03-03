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
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IUserService _userService;

		public AdminController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("get-alls-user")]
		public async Task<IActionResult> GetAllUserAdminPagedAsync([FromQuery]  int page = 1, [FromQuery] int pageSize = 9)
		{
			var result = await _userService.GetAllUserAdminPagedAsync(page, pageSize);
			return Ok(ApiResult<PagedResults<UserResponseModel>>.Success(result));
		}

		[HttpPost("add-user")]
		public async Task<IActionResult> AddUserByAdmin(AddUserAdminModel addUserAdminModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
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
			{
				return BadRequest(ModelState);
			}
			return Ok(ApiResult<UserResponseModel>.Success(await _userService.EditUserAdmin(userRequest)));
		}


	}
}
