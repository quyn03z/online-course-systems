using BusinessLogic.Models;
using DataAccess.Models.CourseModel;
using BusinessLogic.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseController : ControllerBase
	{
		 private readonly ICourseService _courseService;
		public CourseController(ICourseService courseService)
		{
			_courseService = courseService;
		}

		[HttpGet("get-alls-course")]
		public async Task<IActionResult> GetAllsHomeCoursePageAsync(int page, int pageSize)
		{
			return Ok(ApiResult<List<CourseResponseHomeModel>>.Success(await _courseService.GetAllHomeCoursePageAsync(page, pageSize)));
		}

		[HttpGet("get-course-details/{courseId}")]
		public async Task<IActionResult> GetCourseDetailsById(int courseId)
		{
			return Ok(ApiResult<CourseResponseHomeModel>.Success(await _courseService.GetCourseDetailsById(courseId)));
		}



		

	}
}
