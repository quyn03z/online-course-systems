using DataAccess.Models.DashboardModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IAdminRepository
	{
		Task<ChartDataResponse> GetCostChartData();
	}
}
