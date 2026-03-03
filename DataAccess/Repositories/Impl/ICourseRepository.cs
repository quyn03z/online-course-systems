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
		Task<List<CourseResponseHomeModel>> GetAllHomeCoursePageAsync(int page, int pageSize);
		Task<CourseResponseModel> AddCourseAsync(CourseRequestModel courseRequestModel);
		Task<CourseResponseHomeModel> GetCourseDetailsById(int courseId);
		Task<List<CourseResponseModel>> GetAllManaCourseAsync();

		Task<string> RemoveCourseById(int courseId);

		Task<string> UpdateCourseAsync(CourseRequestModel courseRequestModel,int courseId);
	}
}
