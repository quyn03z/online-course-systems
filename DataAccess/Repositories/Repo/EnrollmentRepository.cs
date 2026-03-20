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

		public async Task<bool> AddEnrollmentAsync(EnrollmentModel enrollmentRequestModel)
		{
			try
			{
				var insertedKeys = await _sqlDataAccess.ExecuteQuerySingleAsync<dynamic>("sp_AddEnrollment", enrollmentRequestModel);
				if (insertedKeys != null)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				throw new Exception("sp_AddEnrollment thất bại.", ex);
			}
		}

		public async Task<bool> CheckEnrollmentAsync(EnrollmentModel enrollmentRequestModel)
		{
			try
			{
				var count = await _sqlDataAccess.QueryFirstOrDefaultAsync<int>("sp_CheckEnrollment", enrollmentRequestModel);

				return count > 0;
			}
			catch (Exception ex)
			{
				throw new Exception("sp_CheckEnrollment thất bại.", ex);
			}
		}

		public async Task<List<PurchaseHistoryModel>> PurchaseHistoryByUserIdAsync(int userId)
		{
			try
			{
				var purchase = await _sqlDataAccess.QueryAsync<PurchaseHistoryModel>("sp_PurchaseHistory", new {userId});
				return purchase.ToList();
			}
			catch (Exception ex)
			{
				throw new Exception("sp_PurchaseHistory thất bại.", ex);
			}
		}
	}


}
