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
		public async Task<IActionResult> GetAllsHomeCourse()
		{
			return Ok(ApiResult<List<CourseResponseHomeModel>>.Success(await _courseService.GetAllHomeCourseAsync()));
		}

		[HttpGet("{courseId}")]
		public async Task<IActionResult> GetCourseById(int courseId)
		{
			return Ok(ApiResult<CourseResponseModel>.Success(await _courseService.GetCourseById(courseId)));
		}



		

	}
}
