using DataAccess.Models.CourseModel;
using BusinessLogic.Services.Impl;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository _courseRepository;

		public CourseService(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
		}

		public async Task<CourseResponseModel> AddCourseAsync(CourseRequestModel courseRequestModel)
		{
			return await _courseRepository.AddCourseAsync(courseRequestModel);
		}

		public async Task<List<CourseResponseModel>> GetAllCourseHomeAsync()
		{
			return await _courseRepository.GetAllCourseHomeAsync();
		}

		public Task<List<CourseResponseModel>> GetAllManaCourseAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<CourseResponseModel> GetCourseById(int courseId)
		{
			return await _courseRepository.GetCourseById(courseId);
		}
	}
}
