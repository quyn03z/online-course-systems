using BusinessLogic.Claims;
using BusinessLogic.Services.Impl;
using DataAccess.Models.Enrollment;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class EnrollmentService : IEnrollmentService
	{
		private readonly IEnrollmentRepository _enrollmentRepository;
		private readonly IClaimService _claimService;

		public EnrollmentService(IEnrollmentRepository enrollmentRepository, IClaimService claimService)
		{
			_enrollmentRepository = enrollmentRepository;
			_claimService = claimService;
		}

		public async Task<bool> AddEnrollmentAsync(EnrollmentModel enrollmentRequestModel)
		{
			return await _enrollmentRepository.AddEnrollmentAsync(enrollmentRequestModel);
		}

		public async Task<bool> CheckEnrollmentAsync(EnrollmentModel enrollmentRequestModel)
		{
			return await _enrollmentRepository.CheckEnrollmentAsync(enrollmentRequestModel);
		}

		public async Task<List<PurchaseHistoryModel>> PurchaseHistoryByUserIdAsync()
		{
			var userId = _claimService.GetUserId();
			return await _enrollmentRepository.PurchaseHistoryByUserIdAsync(userId.Value);
		}
	}
}
