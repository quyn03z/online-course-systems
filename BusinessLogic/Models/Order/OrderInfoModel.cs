using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.Order
{
	public class OrderInfoModel
	{
		public string OrderId { get; set; }
		public int UserId { get; set; }
		public int CourseId { get; set; }

		public string FullName { get; set; }

		public string OrderInfo { get; set; }
		public decimal Amount { get; set; }

	}
}
