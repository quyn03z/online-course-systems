using BusinessLogic.Models;
using BusinessLogic.Models.Momo;
using BusinessLogic.Models.Order;
using BusinessLogic.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

	public class PaymentController : BaseController
	{
		private readonly IMomoService _momoService;

		public PaymentController(IMomoService momoService)
		{
			_momoService = momoService;
		}

		[HttpPost("create-momo-payment")]
		public async Task<IActionResult> CreatePaymentMomo(OrderInfoModel model)
		{
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
