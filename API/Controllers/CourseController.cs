using BusinessLogic.Models;
using DataAccess.Models.CourseModel;
using BusinessLogic.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<IActionResult> GetAllsCourse()
		{
			return Ok(ApiResult<List<CourseResponseModel>>.Success(await _courseService.GetAllCourseAsync()));
		}
	}
}
