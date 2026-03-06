using DataAccess.Models.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IEnrollmentService
	{
		Task<bool> AddEnrollmentAsync(EnrollmentModel enrollmentRequestModel);
	}
}
