using DataAccess.Models.CourseModel;
using DataAccess.Models.Enrollment;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IEnrollmentRepository
	{
		Task<bool> AddEnrollmentAsync(EnrollmentModel enrollmentRequestModel);
	}
}
