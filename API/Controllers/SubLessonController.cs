using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.LessonModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class SubLessonController : BaseController
	{
		private readonly ISubLessonService _subLessonService;

		public SubLessonController(ISubLessonService subLessonService)
		{
			_subLessonService = subLessonService;
		}

		[HttpGet("alls-sublesson/{lessonId}")]
		public async Task<IActionResult> GetAllsSubLesson(int lessonId)
		{
			try
			{
				return Ok(ApiResult<List<SubLessonResponseModel>>.Success(await _subLessonService.GetAllsSubLesson(lessonId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả sublesson.");
			}
		}

		[HttpGet("getfirst-sublesson/{lessonId}")]
		public async Task<IActionResult> GetFirstSubLessonByLessonId(int lessonId)
		{
			try
			{
				return Ok(ApiResult<int>.Success(await _subLessonService.GetFirstSubLessonByLessonId(lessonId)));
			}catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả sublesson.");
			}
		}

	}
}
