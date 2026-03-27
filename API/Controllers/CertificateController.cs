using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.CourseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
	public class CertificateController : BaseController
	{
		private readonly ICourseService _courseService;

		public CertificateController(ICourseService courseService)
		{
			_courseService = courseService;
		}

		[HttpGet("download/{courseId}")]
		public async Task<IActionResult> DownloadCertificateAsync(int courseId)
		{
			return Ok(ApiResult<string>.Success(await _courseService.DownloadCertificateAsync(courseId)));
		}



	}
}
