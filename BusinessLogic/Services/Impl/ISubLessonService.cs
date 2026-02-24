using DataAccess.Models.LessonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface ISubLessonService
	{
		Task<List<SubLessonResponseModel>> GetAllsSubLessonAsync(int lessonId);
	}
}
