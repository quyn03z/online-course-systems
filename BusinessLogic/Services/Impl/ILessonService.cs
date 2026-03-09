using DataAccess.Models.LessonModel;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface ILessonService
	{
		Task<IEnumerable<LessonResponseModel>> GetAllManaLessonAsync(int courseId);

		Task<IEnumerable<LessonResponseModel>> GetAllLessonAsync(int courseId);
		Task<LessonResponseModel> AddManaLessonAsync(LessonRequestModel lessonRequesModel,int courseId);
		Task<LessonResponseModel> UpdateManaLessonAsync(int lessonId, LessonRequestModel lessonRequesModel);

		Task<int> GetFirstLessonIdByCourseId(int courseId);

	}
}
