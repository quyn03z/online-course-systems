using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.CourseModel;
using DataAccess.Models.QuestionModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionsController : ControllerBase
	{
		private readonly IQuestionsService _questionsService;

		public QuestionsController(IQuestionsService questionsService)
		{
			_questionsService = questionsService;
		}

		[HttpGet("get-alls-questions/{quizzId}")]
		public async Task<IActionResult> GetAllsQuestionAsync(int quizzId)
		{
			return Ok(ApiResult<List<QuestionResponseModel>>.Success(await _questionsService.GetAllsQuestionAsync(quizzId)));
		}

		[HttpPut("update-questions/{questionId}")]
		public async Task<IActionResult> UpdateQuestionsAsync(int questionId,QuestionRequestModel questionRequestModel)
		{
			return Ok(ApiResult<string>.Success(await _questionsService.UpdateQuestionsAsync(questionId, questionRequestModel)));
		}



	}
}
