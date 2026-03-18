using BusinessLogic.Services.Impl;
using DataAccess.Models.DashboardModel;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class AdminService : IAdminService
	{
		private readonly IUserRepository _userRepository;
		private readonly ICourseRepository _courseRepository;
		private readonly IPaymentRepository _paymentRepository;
		private readonly IAdminRepository _adminRepository;

		public AdminService(IUserRepository userRepository, ICourseRepository courseRepository, IPaymentRepository paymentRepository, IAdminRepository adminRepository)
		{
			_userRepository = userRepository;
			_courseRepository = courseRepository;
			_paymentRepository = paymentRepository;
			_adminRepository = adminRepository;
		}

		public async Task<ChartDataResponse> GetCostChartData()
		{
			return await _adminRepository.GetCostChartData();
		}

		public async Task<InforDashboard> GetInforDashboard()
		{
			int totalUser = await _userRepository.GetTotalsUser();
			int totalCourse = await _courseRepository.GetTotalsCourse();
			decimal totalCost = await _paymentRepository.GetTotalsCost();

			return new InforDashboard
			{
				 totalCost = totalCost,
				 totalUser = totalUser,
				 totalCourse = totalCourse,
			};
		}




	}
}
