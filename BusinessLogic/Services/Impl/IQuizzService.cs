using DataAccess.Models.QuizzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IQuizzService
	{
		Task<QuizzResponseModel> GetQuizzByLessonIdAsync(int lessonId);
		Task<QuizzResponseModel> GetManaQuizzByLessonIdAsync(int lessonId);

		Task<QuizzResponseModel> AddQuizzAsync(QuizzRequestModel quizzRequestModel,int lessonId);
		Task<string> UpdateQuizzAsync(QuizzRequestModel quizzRequestModel, int quizzId);
		Task<string> RemoveQuizzAsync(int quizzId);

	}
}
