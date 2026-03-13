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

		Task<SubLessonResponseModel> AddSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int lessonId,int userId);

		Task<string> UpdateSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int sublessonId,int userId);

		Task<string> RemoveSubLessonAsync(int subLessonId,int userId);


		Task<int> GetFirstSubLessonByLessonId(int lessonId);

	}
}
