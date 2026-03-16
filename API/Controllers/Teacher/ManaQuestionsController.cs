using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.QuestionModel;
using DataAccess.Models.QuizzModel;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Teacher
{
	[Authorize(Roles = AppConstants.Roles.Teacher)]
	public class ManaQuestionsController : BaseController
	{
		private readonly IQuestionsService _questionsService;

		public ManaQuestionsController(IQuestionsService questionsService)
		{
			_questionsService = questionsService;
		}

		[HttpGet("get-alls-questions/{quizzId}")]
		public async Task<IActionResult> GetAllsQuestionAsync(int quizzId)
		{
			return Ok(ApiResult<List<QuestionResponseModel>>.Success(await _questionsService.GetAllsQuestionAsync(quizzId)));
		}

		[HttpPut("update-questions/{questionId}")]
		public async Task<IActionResult> UpdateQuestionsAsync(int questionId, QuizzQuestionsRequestModel quizzQuestionsRequestModel)
		{
			return Ok(ApiResult<string>.Success(await _questionsService.UpdateQuestionsAsync(questionId, quizzQuestionsRequestModel)));
		}

		[HttpPost("add-questions/{quizzId}")]
		public async Task<IActionResult> AddQuestionsAsync(int quizzId, QuestionRequestModel questionRequestModel)
		{
			return Ok(ApiResult<QuestionResponseModel>.Success(await _questionsService.AddQuestionsAsync(quizzId, questionRequestModel)));
		}

		[HttpDelete("delete-questions/{questionId}")]
		public async Task<IActionResult> DeleteQuestionsAsync(int questionId)
		{
			return Ok(ApiResult<string>.Success(await _questionsService.DeleteQuestionsAsync(questionId)));
		}



	}
}
