using BusinessLogic.Services.Impl;
using DataAccess.Models.PaymentModel;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class PaymentService : IPaymentService
	{
		private readonly IPaymentRepository _paymentRepository;

		public PaymentService(IPaymentRepository paymentRepository)
		{
			_paymentRepository = paymentRepository;
		}

		public async Task<bool> AddPaymentAsync(PaymentRequestModel paymentRequestModel)
		{
			 return await _paymentRepository.AddPaymentAsync(paymentRequestModel);
		}
	}
}
