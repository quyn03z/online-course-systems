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
		Task<List<CourseResponseModel>> GetAllCourseHomeAsync();
		Task<CourseResponseModel> AddCourseAsync(CourseRequestModel courseRequestModel);
		Task<CourseResponseModel> GetCourseById(int courseId);
	}
}
