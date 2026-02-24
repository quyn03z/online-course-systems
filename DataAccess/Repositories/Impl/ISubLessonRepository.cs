using DataAccess.Models.LessonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface ISubLessonRepository
	{
		// lấy hết cả lock
		Task<List<SubLessonResponseModel>> GetAllsSubLessonAsync(int lessonId);

		// lấy hết không có lock
	}
}
