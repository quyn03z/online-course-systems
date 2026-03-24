using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.MenteeScoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
	[Authorize]
	public class MenteeScoreController : BaseController
	{
		private readonly IMenteeScoreService _menteeScoreService;

		public MenteeScoreController(IMenteeScoreService menteeScoreService)
		{
			_menteeScoreService = menteeScoreService;
		}

		[HttpPost("add-menteeScore")]
		public async Task<IActionResult> AddMenteeScoreAsync(MenteeScoreRequestModel menteeScoreRequestModel)
		{
			if (!ModelState.IsValid)
				return ValidationError();
			return Ok(ApiResult<MenteeScoreRequestModel>.Success(await _menteeScoreService.AddMenteeScoreAsync(menteeScoreRequestModel)));
		}
		
		[HttpGet("get-progress/{courseId}")]
		public async Task<IActionResult> GetProgressAsync(int courseId)
		{
			return Ok(ApiResult<CheckProgressModel>.Success(await _menteeScoreService.GetProgressAsync(courseId)));
		}
	}
}
