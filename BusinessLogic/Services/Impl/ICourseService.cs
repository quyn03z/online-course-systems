using DataAccess.Models.CourseModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface ICourseService
	{
		Task<List<CourseResponseModel>> GetAllCourseHomeAsync();
		Task<CourseResponseModel> AddCourseAsync(CourseRequestModel courseRequestModel);
		Task<CourseResponseModel> GetCourseById(int courseId);

		Task<List<CourseResponseModel>> GetAllManaCourseAsync();
	}
}
