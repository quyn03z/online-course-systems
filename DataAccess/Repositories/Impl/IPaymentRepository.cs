using DataAccess.Models.Enrollment;
using DataAccess.Models.PaymentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IPaymentRepository
	{
		Task<bool> AddPaymentAsync(PaymentRequestModel paymentRequestModel);

	}
}
