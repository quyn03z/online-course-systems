using Azure.Core;
using BusinessLogic.Claims;
using BusinessLogic.Models;
using BusinessLogic.Models.Momo;
using BusinessLogic.Models.Order;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.Enrollment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

	public class PaymentController : BaseController
	{
		private readonly IMomoService _momoService;
		private readonly ICourseService _courseService;
		private readonly IEnrollmentService _enrollmentService;
		private readonly IClaimService _claimService;

		public PaymentController(IMomoService momoService, ICourseService courseService, IEnrollmentService enrollmentService, IClaimService claimService)
		{
			_momoService = momoService;
			_courseService = courseService;
			_enrollmentService = enrollmentService;
			_claimService = claimService;
		}

		[HttpPost("create-momo-payment")]
		public async Task<IActionResult> CreatePaymentMomo(OrderInfoModel model)
		{
			var course = await _courseService.GetCourseDetailsById(model.CourseId);
			var ipAddress = _claimService.GetIpAddress();
			var userAgent = _claimService.GetUserAgent();

			if (course.Price == 0)
			{
				var enrollModel = new EnrollmentModel
				{
					UserId = _claimService.GetUserId().Value,
					CourseId = model.CourseId,
					IpAddress = ipAddress,
					UserAgent = userAgent,
				};

				var stopwatch = System.Diagnostics.Stopwatch.StartNew();
				// Kiểm tra đã đăng ký chưa để tránh lỗi trùng khóa
				bool alreadyEnrolled = await _enrollmentService.CheckEnrollmentAsync(enrollModel);
				if (alreadyEnrolled)
					return Ok(ApiResult<object>.Success(null, "Bạn đã đăng ký khóa học này rồi!"));
				stopwatch.Stop();
				enrollModel.DurationMs = (int)stopwatch.ElapsedMilliseconds;

				bool isEnrolled = await _enrollmentService.AddEnrollmentAsync(enrollModel);
				

				if (isEnrolled)
					return Ok(ApiResult<object>.Success(null, "Đăng ký khóa học miễn phí thành công!"));
				else
					return BadRequest(ApiResult<object>.Failure("Đăng ký thất bại."));
			}


			var response = await _momoService.CreatePaymentAsync(model);
			if (response == null || response.ErrorCode != 0 || string.IsNullOrEmpty(response.PayUrl))
			{
				string errorMessage = response?.Message ?? "Có lỗi xảy ra khi tạo thanh toán.";
				return BadRequest(ApiResult<MomoCreatePaymentResponeModel>.Failure(errorMessage));
			}
			return Ok(ApiResult<MomoCreatePaymentResponeModel>.Success(response));
		}





	}
}
