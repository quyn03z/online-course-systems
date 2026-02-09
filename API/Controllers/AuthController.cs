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

		[HttpPost("logout")]
		public async Task<IActionResult> LogoutAsync()
		{
			var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
			{
				return Unauthorized(ApiResult<object>.Failure(new[] { "User not authenticated" }));
			}
			await _userService.LogoutAsync(userId);
			return Ok(ApiResult<object>.Success(new { message = "Đã đăng xuất thành công" }));
		}

		[HttpPost("forgot-password")]
		public async Task<IActionResult> ForgotPasswordAsync(EmailRequest email)
		{
			return Ok(ApiResult<ForgotPassWordModel>
				.Success(await _userService.ForgotPasswordAsync(email)));
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(ApiResult<ResetPasswordModel>
				.Success(await _userService.ResetPasswordAsync(resetPasswordModel)));
		}


	}
}
