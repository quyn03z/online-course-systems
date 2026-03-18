using DataAccess.Models.CourseModel;
using DataAccess.Models.DashboardModel;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class AdminRepository : IAdminRepository
	{
		private readonly ISqlDataAccess _sqlDataAccess;

		public AdminRepository(ISqlDataAccess sqlDataAccess)
		{
			_sqlDataAccess = sqlDataAccess;
		}

		public async Task<ChartDataResponse> GetCostChartData()
		{
			try
			{
				var rawData = await _sqlDataAccess.QueryAsync<MonthlyRevenue>("sp_GetMonthlyRevenue");
				var chartResponse = new ChartDataResponse
				{
					Labels = rawData.Select(x => (string)x.MonthName).ToList(),
					Data = rawData.Select(x => (decimal)x.TotalRevenue).ToList()
				};
				return chartResponse;
			}
			catch (Exception ex)
			{
				throw new Exception("Không thể lấy dữ liệu biểu đồ doanh thu.", ex);
			}
		}


	}
}
