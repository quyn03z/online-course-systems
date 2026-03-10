using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.CourseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class CourseTypeController : BaseController
	{
		private readonly ICourseTypeService _courseTypeService;

		public CourseTypeController(ICourseTypeService courseTypeService)
		{
			_courseTypeService = courseTypeService;
		}

		[HttpGet("alls-courseType")]
		public async Task<IActionResult> GetAllsCourseTypeAsync()
		{
			return Ok(ApiResult<List<CourseResponseTypeModel>>.Success(await _courseTypeService.GetAllsCourseTypeAsync()));
		}
	}
}
