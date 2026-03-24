using DataAccess.Models.CourseModel;
using DataAccess.Models.Enrollment;
using DataAccess.Models.PageResultModel;
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

		Task<bool> CheckEnrollmentAsync(EnrollmentModel enrollmentRequestModel);

		Task<List<PurchaseHistoryModel>> PurchaseHistoryByUserIdAsync(int userId);

		Task<List<User>> AllsUserCourseAsync(int courseId,int page, int pageSize, string? search = null);


	}
}
