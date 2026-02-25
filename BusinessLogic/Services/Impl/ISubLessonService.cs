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

		Task<List<SubLessonResponseModel>> GetAllsSubLesson(int lessonId);

		Task<SubLessonResponseModel> AddSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int lessonId);

		Task<string> UpdateSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int sublessonId);

		Task<string> RemoveSubLessonAsync(int sublessonId);

	}
}
