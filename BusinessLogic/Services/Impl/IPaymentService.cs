using DataAccess.Models.Enrollment;
using DataAccess.Models.PaymentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IPaymentService
	{
		Task<bool> AddaymentAsync(PaymentRequestModel paymentRequestModel);

	}
}
