using DataAccess.Models.QuestionModel;
using DataAccess.Models.QuizzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IQuestionsRepository
	{
		Task<List<QuestionResponseModel>> GetAllsQuestionAsync(int quizzId);

		Task<string> UpdateQuestionsAsync(int questionId, QuizzQuestionsRequestModel quizzRequestModel);

		Task<QuestionResponseModel> AddQuestionsAsync(int quizzId,int userId ,QuestionRequestModel questionRequestModel);

		Task<string> DeleteQuestionsAsync(int quizzId, int userId);
	}
}
