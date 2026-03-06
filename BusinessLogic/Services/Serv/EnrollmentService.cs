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

		public EnrollmentService(IEnrollmentRepository enrollmentRepository)
		{
			_enrollmentRepository = enrollmentRepository;
		}

		public async Task<bool> AddEnrollmentAsync(EnrollmentModel enrollmentRequestModel)
		{
			return await _enrollmentRepository.AddEnrollmentAsync(enrollmentRequestModel);
		}



	}
}
