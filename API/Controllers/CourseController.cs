using BusinessLogic.Models;
using DataAccess.Models.CourseModel;
using BusinessLogic.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using DataAccess.Models.CourseType;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseController : ControllerBase
	{
		 private readonly ICourseService _courseService;
		 private readonly ICourseTypeService _courseTypeService;

		public CourseController(ICourseService courseService, ICourseTypeService courseTypeService)
		{
			_courseService = courseService;
			_courseTypeService = courseTypeService;
		}

		[HttpGet("get-alls-course")]
		public async Task<IActionResult> GetAllsHomeCoursePageAsync(int page, int pageSize, int? courseTypeId, int? priceOrder, string search = "")
		{
			return Ok(ApiResult<List<CourseResponseHomeModel>>.Success(await _courseService.GetAllHomeCoursePageAsync(page, pageSize,courseTypeId,priceOrder,search)));
		}

		[HttpGet("get-course-details/{courseId}")]
		public async Task<IActionResult> GetCourseDetailsById(int courseId)
		{
			return Ok(ApiResult<CourseResponseHomeModel>.Success(await _courseService.GetCourseDetailsById(courseId)));
		}


		[HttpGet("alls-courseType")]
		public async Task<IActionResult> GetAllsCoursetypeAsync()
		{
			return Ok(ApiResult<List<CourseResponseTypeModel>>.Success(await _courseTypeService.GetAllsCourseTypeAsync()));
		}

	}
}
