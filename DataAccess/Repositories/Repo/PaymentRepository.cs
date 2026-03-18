using DataAccess.Models.CourseModel;
using DataAccess.Models.PaymentModel;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class PaymentRepository : IPaymentRepository
	{
		private readonly ISqlDataAccess _sqlDataAccess;

		public PaymentRepository(ISqlDataAccess sqlDataAccess)
		{
			_sqlDataAccess = sqlDataAccess;
		}

		public async Task<bool> AddPaymentAsync(PaymentRequestModel paymentRequestModel)
		{
			try
			{
				int paymentId = await _sqlDataAccess.ExecuteSalarAsync<int>("sp_AddPayment", paymentRequestModel);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("Payment thất bại.", ex);
			}
		}

		public async Task<decimal> GetTotalsCost()
		{
			try
			{
				return await _sqlDataAccess.ExecuteSalarAsync<int>("sp_GetTotalsCost");
			}
			catch (Exception ex)
			{
				throw new Exception("Không thể lấy totals", ex);
			}
		}
	}
}
