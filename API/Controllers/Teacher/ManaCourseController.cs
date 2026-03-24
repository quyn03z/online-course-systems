using API.Filter;
using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.CourseModel;
using DataAccess.Models.PageResultModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers.Teacher
{
	[Authorize(Roles = AppConstants.Roles.Teacher)]
	public class ManaCourseController : BaseController
	{
		private readonly ICourseService _courseService;
		private readonly IUserService _userService;

		public ManaCourseController(ICourseService courseService, IUserService userService)
		{
			_courseService = courseService;
			_userService = userService;
		}

		[HttpGet("get-alls-mana-course")]
		public async Task<IActionResult> GetAllsCourse(int page, int pageSize,string search = "")
		{
			try
			{
				return Ok(ApiResult<List<CourseResponseModel>>.Success(await _courseService.GetAllManaCourseByUserIdAsync(page,pageSize,search)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả khóa học.");
			}
		}

		[HttpPost("add-course")]
		[Permission("course.create")]
		public async Task<IActionResult> AddCourseAsync(CourseRequestModel courseRequestModel)
		{
			try
			{
				if (!ModelState.IsValid)
				return ValidationError();
				return Ok(ApiResult<CourseResponseModel>.Success(await _courseService.AddCourseAsync(courseRequestModel)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi thêm khóa học.");
			}
		}

		[HttpDelete("remove-course/{courseId}")]
		[Permission("course.delete")]
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
		[Permission("course.edit")]
		public async Task<IActionResult> UpdateCourseAsync(CourseRequestModel courseRequestModel, int courseId)
		{
			try
			{
				if(!ModelState.IsValid)
				return ValidationError();
				return Ok(ApiResult<string>.Success(await _courseService.UpdateCourseAsync(courseRequestModel,courseId)));
			}catch (Exception ex)
			{
				throw new Exception("Có lỗi khi cập nhật khóa học.");
			}
		}


		//[HttpGet("alls-user-course")]
		//public async Task<IActionResult> AllsUserCourseAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
		//{
		//	var result = await _userService.AllsUserCourseAsync(page, pageSize, search);
		//	return Ok(ApiResult<PagedResults<UserResponseModel>>.Success(result));
		//}

	

	}
}
