using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.LessonModel;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
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

		

		[HttpPost("add-sublesson")]
		public async Task<IActionResult> AddSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int lessonId)
		{
			try
			{
				return Ok(ApiResult<SubLessonResponseModel>.Success(await _subLessonService.AddSubLessonAsync(subLessonRequestModel, lessonId)));
			}catch (Exception ex)
			{
				throw new Exception("Có lỗi khi thêm sublesson.");
			}
		}


		[HttpPut("update-sublesson/{sublessonId}")]
		public async Task<IActionResult> UpdateSubLessonAsync(SubLessonRequestModel subLessonRequestModel,int sublessonId)
		{
			try
			{
				return Ok(ApiResult<string>.Success(await _subLessonService.UpdateSubLessonAsync(subLessonRequestModel, sublessonId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi update sublesson.");
			}
		}

		[HttpDelete("remove-sublesson/{lessonId}")]
		public async Task<IActionResult> RemoveSubLessonAsync(int lessonId)
		{
			try
			{
				return Ok(ApiResult<string>.Success(await _subLessonService.RemoveSubLessonAsync(lessonId)));
			}catch (Exception ex)
			{
				throw new Exception("Có lỗi khi delete sublesson.");
			}
		}



	}

}
