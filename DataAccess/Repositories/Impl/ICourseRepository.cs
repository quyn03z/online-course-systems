using DataAccess.Models.CourseModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface ICourseRepository 
	{
		Task<List<CourseResponseHomeModel>> GetAllHomeCoursePageAsync(int page, int pageSize, int? courseTypeId, int? priceOrder, string search = "");
		Task<CourseResponseModel> AddCourseAsync(CourseRequestModel courseRequestModel,int userId);
		Task<CourseResponseHomeModel> GetCourseDetailsById(int courseId);
		Task<List<CourseResponseModel>> GetAllManaCourseByUserIdAsync(int userId,int page, int pageSize, string search = "");
		Task<string> RemoveCourseById(int courseId,int userId);
		Task<string> UpdateCourseAsync(CourseRequestModel courseRequestModel,int courseId,int userId);

		Task<int> GetTotalsCourse();
	}
}
