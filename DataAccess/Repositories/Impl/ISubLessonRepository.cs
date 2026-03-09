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
		// lấy hết cả locked
		Task<List<SubLessonResponseModel>> GetAllsSubLessonAsync(int lessonId);

		// lấy hết không có locked

		Task<List<SubLessonResponseModel>> GetAllsSubLesson(int lessonId);

		Task<SubLessonResponseModel> AddSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int lessonId);

		Task<string> UpdateSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int sublessonId);

		Task<string> RemoveSubLessonAsync(int subLessonId);


		Task<int> GetFirstSubLessonByLessonId(int lessonId);

	}
}
