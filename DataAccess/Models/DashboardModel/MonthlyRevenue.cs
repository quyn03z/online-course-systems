using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.DashboardModel
{
	public class MonthlyRevenue
	{
		public string MonthName { get; set; }
		public decimal TotalRevenue { get; set; }
	}

	public class ChartDataResponse
	{
		public List<string> Labels { get; set; } = new List<string>();
		public List<decimal> Data { get; set; } = new List<decimal>();
	}
}
