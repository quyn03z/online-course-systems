using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserService _userService;

		public AuthController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("create-user")]
		public async Task<IActionResult> CreateUserAsync(CreateUserModel createUserModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(ApiResult<CreateUserResponseModel>
				.Success(await _userService.CreateUserAsync(createUserModel)));
		}


		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync(LoginUserModel loginUserModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);	
			}
			return Ok(ApiResult<LoginResponseModel>
				.Success(await _userService.LoginAsync(loginUserModel)));

		}




	}
}
