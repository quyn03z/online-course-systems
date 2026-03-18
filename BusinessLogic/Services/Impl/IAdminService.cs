using DataAccess.Models.DashboardModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IAdminService
	{
		Task<InforDashboard> GetInforDashboard();

		Task<ChartDataResponse> GetCostChartData();

	}
}
