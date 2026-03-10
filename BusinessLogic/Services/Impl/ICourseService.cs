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
		Task<List<CourseResponseHomeModel>> GetAllHomeCoursePageAsync(int page, int pageSize);
		Task<CourseResponseModel> AddCourseAsync(CourseRequestModel courseRequestModel);
		Task<CourseResponseHomeModel> GetCourseDetailsById(int courseId);

		Task<List<CourseResponseModel>> GetAllManaCourseAsync(int page, int pageSize, string search = "");

		Task<string> RemoveCourseById(int courseId);

		Task<string> UpdateCourseAsync(CourseRequestModel courseRequestModel, int courseId);
	}
}
