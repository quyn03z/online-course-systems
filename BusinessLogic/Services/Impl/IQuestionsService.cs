using DataAccess.Models.QuestionModel;
using DataAccess.Models.QuizzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IQuestionsService
	{
		Task<List<QuestionResponseModel>> GetAllsQuestionAsync(int quizzId);

		Task<string> UpdateQuestionsAsync(int questionId, QuizzQuestionsRequestModel quizzRequestModel);

		Task<QuestionResponseModel> AddQuestionsAsync(int quizzId, QuestionRequestModel questionRequestModel);

		Task<string> DeleteQuestionsAsync(int quizzId);

	}
}
