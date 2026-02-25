using DataAccess.Models.LessonModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface ILessonRepository : IBaseRepository<Lesson>
	{
		// get alls lesson by courseId
		Task<IEnumerable<Lesson>> GetAllManaLessonAsync(int courseId);


	}
}
