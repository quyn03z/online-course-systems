using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class CourseTypeRepository : BaseRepository<CourseType>, ICourseTypeRepository
	{
		public CourseTypeRepository(OCMSMSFContext context) : base(context)
		{
		}




	}
}
