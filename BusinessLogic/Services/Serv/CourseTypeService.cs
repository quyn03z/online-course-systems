using BusinessLogic.Services.Impl;
using DataAccess.Models.CourseType;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class CourseTypeService : ICourseTypeService
	{
		private readonly ICourseTypeRepository _courseTypeRepository;

		public CourseTypeService(ICourseTypeRepository courseTypeRepository)
		{
			_courseTypeRepository = courseTypeRepository;
		}

		public async Task<List<CourseResponseTypeModel>> GetAllsCourseTypeAsync()
		{
			var courseType = await _courseTypeRepository.GetAll();
			return courseType.Select(x => new CourseResponseTypeModel
			{
				CourseTypeId = x.CourseTypeId,
				Name = x.Name,
			}).ToList();
		}

	}
}
