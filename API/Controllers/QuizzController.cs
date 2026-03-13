using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.QuizzModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{

	public class QuizzController : BaseController
	{
		private readonly IQuizzService  _quizzService;
		public QuizzController(IQuizzService quizzService)
		{
			_quizzService = quizzService;
		}

		[HttpGet("get-alls-quizz/{lessonId}")]
		public async Task<IActionResult> GetQuizzByLessonIdAsync(int lessonId)
		{
			return Ok(ApiResult<QuizzResponseModel>.Success(await _quizzService.GetQuizzByLessonIdAsync(lessonId)));
		}

		


	}
}
