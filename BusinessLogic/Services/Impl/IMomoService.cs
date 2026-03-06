using BusinessLogic.Models.Momo;
using BusinessLogic.Models.Order;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IMomoService
	{
		Task<MomoCreatePaymentResponeModel> CreatePaymentAsync(OrderInfoModel model);
		Task<MomoExecuteResponseModel> PaymentExecuteAsync(IQueryCollection collection);

	}
}
