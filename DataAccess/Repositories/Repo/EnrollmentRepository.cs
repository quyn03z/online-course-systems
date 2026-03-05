using DataAccess.Models.CourseModel;
using DataAccess.Models.Enrollment;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class EnrollmentRepository : IEnrollmentRepository
	{
		private readonly ISqlDataAccess _sqlDataAccess;

		public EnrollmentRepository(ISqlDataAccess sqlDataAccess)
		{
			_sqlDataAccess = sqlDataAccess;
		}

		public async Task<EnrollmentModel> AddEnrollmentAsync(EnrollmentModel enrollmentRequestModel)
		{
			try
			{
				var insertedKeys = await _sqlDataAccess.ExecuteQuerySingleAsync<dynamic>("sp_InsertCourseRegistration",new { UserId = 1, CourseId = 5 });
				return new EnrollmentModel
				{
					CourseId = insertedKeys.CourseId,
					UserId = insertedKeys.UserId,
				};
			}
			catch (Exception ex)
			{
				throw new Exception("Enrollment thất bại.", ex);
			}
		}

	}
}
