using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.PaymentModel
{
	public class PaymentRequestModel
	{
		public int? UserId { get; set; }

		public int? CourseId { get; set; }

		public decimal? Amount { get; set; }
		public string TransactionCode { get; set; }
	}

	public class PaymentResponseModel
	{
		public int PaymentId { get; set; }

		public int? UserId { get; set; }

		public int? CourseId { get; set; }

		public decimal? Amount { get; set; }
		public string TransactionCode { get; set; }
	}
}
