using API.Filter;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.QuizzModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Teacher
{
	public class ManaQuizzController : BaseController
	{
		private readonly IQuizzService _quizzService;
		public ManaQuizzController(IQuizzService quizzService)
		{
			_quizzService = quizzService;
		}

		[HttpGet("get-mana-quizz/{lessonId}")]
		public async Task<IActionResult> GetManaQuizzByLessonIdAsync(int lessonId)
		{
			return Ok(ApiResult<QuizzResponseModel>.Success(await _quizzService.GetManaQuizzByLessonIdAsync(lessonId)));
		}


		[HttpPost("add-quizz/{lessonId}")]
		[Permission("quiz.create")]
		public async Task<IActionResult> AddQuizzAsync(QuizzRequestModel quizzRequestModel, int lessonId)
		{
			try
			{
				if (!ModelState.IsValid)
					return ValidationError();
				return Ok(ApiResult<QuizzResponseModel>.Success(await _quizzService.AddQuizzAsync(quizzRequestModel, lessonId)));
			}
			catch (Exception ex)
			{
				throw new BadRequestException("Mỗi Lesson chỉ có thể tạo một Quiz duy nhất.");
			}
		}

		[HttpPut("update-quizz/{quizzId}")]
		[Permission("quiz.edit")]
		public async Task<IActionResult> UpdateQuizzAsync(QuizzRequestModel quizzRequestModel, int quizzId)
		{
			if (!ModelState.IsValid)
				return ValidationError();
			return Ok(ApiResult<string>.Success(await _quizzService.UpdateQuizzAsync(quizzRequestModel, quizzId)));
		}

		[HttpDelete("remove-quizz/{quizzId}")]
		[Permission("quiz.delete")]
		public async Task<IActionResult> RemoveQuizzAsync(int quizzId)
		{
			return Ok(ApiResult<string>.Success(await _quizzService.RemoveQuizzAsync(quizzId)));
		}

	}
}
