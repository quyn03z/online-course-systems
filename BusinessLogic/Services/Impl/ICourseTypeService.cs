using DataAccess.Models.CourseType;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface ICourseTypeService
	{
		Task<List<CourseResponseTypeModel>>  GetAllsCourseTypeAsync();

	}
}
