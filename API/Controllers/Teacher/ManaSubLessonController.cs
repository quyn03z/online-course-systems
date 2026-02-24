using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.LessonModel;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Teacher
{
	[Route("api/[controller]")]
	[ApiController]
	public class ManaSubLessonController : ControllerBase
	{
		private readonly ISubLessonService _subLessonService;
		public ManaSubLessonController(ISubLessonService subLessonService)
		{
			_subLessonService = subLessonService;
		}

		[HttpGet("get-alls-sublesson/{lessonId}")]
		public async Task<IActionResult> GetAllsSubLessonAsync(int lessonId)
		{
			try
			{
				return Ok(ApiResult<List<SubLessonResponseModel>>.Success(await _subLessonService.GetAllsSubLessonAsync(lessonId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả sublesson.");
			}
		}



	}

}
