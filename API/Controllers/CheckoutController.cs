using BusinessLogic.Claims;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.CourseModel;
using DataAccess.Models.Enrollment;
using DataAccess.Models.PaymentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class CheckoutController : BaseController
	{
		private IMomoService _momoService;
		private IUserService _userService;
		private IPaymentService _paymentService;
		private IEnrollmentService _enrollmentService;
		private IClaimService _claimService;

		public CheckoutController(IMomoService momoService, IUserService userService, IPaymentService paymentService, IEnrollmentService enrollmentService, IClaimService claimService)
		{
			_momoService = momoService;
			_userService = userService;
			_paymentService = paymentService;
			_enrollmentService = enrollmentService;
			_claimService = claimService;
		}

		[Authorize]
		[HttpGet("PaymentCallBack")]
		public async Task<IActionResult> PaymentCallBack()
		{
			var requestQuery = HttpContext.Request.Query;

			var response = await _momoService.PaymentExecuteAsync(requestQuery);
			// lấy userId
			var userId = _claimService.GetUserId();

			string errorCode = requestQuery["errorCode"];
			string orderId = requestQuery["orderId"];
            string message = requestQuery["message"];
			 

			bool isSuccess = (errorCode == "0");

			if (isSuccess  && response.CourseId > 0)
			{
				// 1. Lưu Enrollment
				await _enrollmentService.AddEnrollmentAsync(new EnrollmentModel
				{
					UserId = userId.Value,
					CourseId = (int)response.CourseId
				});

				// 2. Lưu Payment
				await _paymentService.AddaymentAsync(new PaymentRequestModel
				{
					UserId = userId.Value,
					CourseId = (int)response.CourseId,
					Amount = decimal.TryParse(response.Amount, out var amt) ? amt : 0,
					TransactionCode = orderId
				});

				return Ok(ApiResult<object>.Success(response, "Thanh toán và đăng ký khóa học thành công!"));
			}

            return BadRequest(ApiResult<object>.Failure(string.IsNullOrEmpty(message) ? "Thanh toán thất bại." : message));
		}

		[Authorize]
		[HttpGet("check-enrollment")]
		public async Task<IActionResult> CheckEnrollmentAsync(int courseId)
		{
			var userId = _claimService.GetUserId();

			var enrollmentModel = new EnrollmentModel
			{
				UserId = userId.Value,
				CourseId = courseId
			};

			var isEnrolled = await _enrollmentService.CheckEnrollmentAsync(enrollmentModel);
			return Ok(ApiResult<bool>.Success(isEnrolled));
		}


	}
}
