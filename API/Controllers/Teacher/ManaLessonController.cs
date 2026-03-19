using API.Filter;
using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.CourseModel;
using DataAccess.Models.LessonModel;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Teacher
{
	[Authorize(Roles = AppConstants.Roles.Teacher)]
	public class ManaLessonController : BaseController
	{
		private readonly ILessonService _lessonService;

		public ManaLessonController(ILessonService lessonService)
		{
			_lessonService = lessonService;
		}

		[HttpGet("get-alls-lesson/{courseId}")]
		public async Task<IActionResult> GetAllsLesson(int courseId)
		{
			try
			{
				return Ok(ApiResult<IEnumerable<LessonResponseModel>>.Success(await _lessonService.GetAllManaLessonAsync(courseId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả bài học.");
			}
		}


		[HttpPost("add-lesson/{courseId}")]
		[Permission("lesson.create")]
		public async Task<IActionResult> AddLessonAsync(LessonRequestModel lessonRequesModel,int courseId)
		{
			try
			{
				if (!ModelState.IsValid)
					return ValidationError();
				return Ok(ApiResult<LessonResponseModel>.Success(await _lessonService.AddManaLessonAsync(lessonRequesModel,courseId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy thêm bài học.");
			}
		}

		[HttpPut("update-lesson/{lessonId}")]
		[Permission("lesson.eidt")]
		public async Task<IActionResult> UpdateLessonAsync(int lessonId, LessonRequestModel lessonRequesModel)
		{
			try
			{
				if (!ModelState.IsValid)
					return ValidationError();
				return Ok(ApiResult<LessonResponseModel>.Success(await _lessonService.UpdateManaLessonAsync(lessonId, lessonRequesModel)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi cập nhật bài học.");
			}
		}

		[HttpDelete("remove-lesson/{lessonId}")]
		[Permission("lesson.delete")]
		public async Task<IActionResult> RemoveLessonAsync(int lessonId)
		{
			try
			{
				return Ok(ApiResult<string>.Success(await _lessonService.RemoveLessonAsync(lessonId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi xóa khóa học.");
			}
		}
	}
}
