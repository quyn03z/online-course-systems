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

		Task<string> UpdateQuestionsAsync(int questionId, int userId ,QuizzQuestionsRequestModel quizzQuestionsRequestModel);

		Task<QuestionResponseModel> AddQuestionsAsync(int quizzId,int userId ,QuestionRequestModel questionRequestModel);

		Task<string> DeleteQuestionsAsync(int questionId, int userId);
	}
}
