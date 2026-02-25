using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.LessonModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LessonController : ControllerBase
	{
		private readonly ILessonService _lessonService;
		public LessonController(ILessonService lessonService)
		{
			_lessonService = lessonService;
		}

		[HttpGet("get-alls-lesson/{courseId}")]
		public async Task<IActionResult> GetAllLessonAsync(int courseId)
		{
			try
			{
				return Ok(ApiResult<IEnumerable<LessonResponseModel>>.Success(await _lessonService.GetAllLessonAsync(courseId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả bài học.");
			}
		}
	}
}
