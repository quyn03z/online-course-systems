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

		public async Task<List<CourseResponseHomeModel>> GetAllHomeCoursePageAsync(int page, int pageSize)
		{
			return await _courseRepository.GetAllHomeCoursePageAsync(page, pageSize);
		}

		public async Task<List<CourseResponseModel>> GetAllManaCourseAsync()
		{
			return await _courseRepository.GetAllManaCourseAsync();
		}

		public async Task<CourseResponseHomeModel> GetCourseDetailsById(int courseId)
		{
			return await _courseRepository.GetCourseDetailsById(courseId);
		}

		public async Task<string> RemoveCourseById(int courseId)
		{
			return await _courseRepository.RemoveCourseById(courseId);
		}

		public async Task<string> UpdateCourseAsync(CourseRequestModel courseRequestModel, int courseId)
		{
			return await _courseRepository.UpdateCourseAsync(courseRequestModel, courseId);
		}


	}
}
