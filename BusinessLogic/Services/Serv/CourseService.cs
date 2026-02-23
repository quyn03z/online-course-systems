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

		public async Task<List<CourseResponseModel>> GetAllCourseAsync()
		{
			var allsCourse = await _courseRepository.GetAllCourseAsync();
			return allsCourse.Select(x => new CourseResponseModel
			{
				CourseId = x.CourseId,
				CourseName = x.CourseName,
				Title = x.Title,
				Description = x.Description,
				Image = x.Image,
				IsDelete = x.IsDelete,
				IsLocked = x.IsLocked,
				Price = x.Price,
				CourseTypeName = x.CourseTypeName,

			}).ToList();
		}


	}
}
