using DataAccess.Models.DashboardModel;
using DataAccess.Models.Enrollment;
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
		Task<TopCourseModel> GetTopCourseEnrollment();
	}
}
