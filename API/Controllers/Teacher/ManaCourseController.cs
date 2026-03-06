using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.CourseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Teacher
{
	[Route("api/[controller]")]
	[Authorize(Roles = AppConstants.Roles.Teacher)]
	[ApiController]
	public class ManaCourseController : ControllerBase
	{
		private readonly ICourseService _courseService;

		public ManaCourseController(ICourseService courseService)
		{
			_courseService = courseService;
		}

		[HttpGet("get-alls-mana-course")]
		public async Task<IActionResult> GetAllsCourse()
		{
			try
			{
				return Ok(ApiResult<List<CourseResponseModel>>.Success(await _courseService.GetAllManaCourseAsync()));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả khóa học.");
			}
		}

		[HttpPost("add-course")]
		public async Task<IActionResult> AddCourseAsync(CourseRequestModel courseRequestModel)
		{
			try
			{
				return Ok(ApiResult<CourseResponseModel>.Success(await _courseService.AddCourseAsync(courseRequestModel)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi thêm khóa học.");
			}
		}

		[HttpDelete("remove-course/{courseId}")]
		public async Task<IActionResult> RemoveCourseAsync(int courseId)
		{
			try
			{
				return Ok(ApiResult<string>.Success(await _courseService.RemoveCourseById(courseId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi xóa khóa học.");
			}
		}

		[HttpPut("update-course/{courseId}")]
		public async Task<IActionResult> UpdateCourseAsync(CourseRequestModel courseRequestModel, int courseId)
		{
			try
			{
				return Ok(ApiResult<string>.Success(await _courseService.UpdateCourseAsync(courseRequestModel,courseId)));
			}catch (Exception ex)
			{
				throw new Exception("Có lỗi khi cập nhật khóa học.");
			}
		}


	}
}
