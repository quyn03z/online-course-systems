using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.QuizzModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuizzController : ControllerBase
	{
		private readonly IQuizzService  _quizzService;
		public QuizzController(IQuizzService quizzService)
		{
			_quizzService = quizzService;
		}

		[HttpGet("get-alls-quizz")]
		public async Task<IActionResult> GetAllQuizzAsync()
		{
			return Ok(ApiResult<IEnumerable<QuizzResponseModel>>
				.Success(await _quizzService.GetAllQuizzAsync()));
		}

		[HttpPost("add-quizz")]
		public async Task<IActionResult> AddQuizzAsync(QuizzRequestModel quizzRequestModel, int lessonId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(ApiResult<QuizzResponseModel>.Success(await _quizzService.AddQuizzAsync(quizzRequestModel, lessonId)));
		}

		[HttpPut("update-quizz")]
		public async Task<IActionResult> UpdateQuizzAsync(QuizzRequestModel quizzRequestModel, int quizzId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(ApiResult<string>.Success(await _quizzService.UpdateQuizzAsync(quizzRequestModel, quizzId)));
		}

		[HttpDelete("remove-quizz")]
		public async Task<IActionResult> RemoveQuizzAsync(int quizzId)
		{
			return Ok(ApiResult<string>.Success(await _quizzService.RemoveQuizzAsync(quizzId)));
		}


	}
}
