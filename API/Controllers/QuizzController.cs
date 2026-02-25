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


	}
}
