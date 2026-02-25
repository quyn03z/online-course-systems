using DataAccess.Models.QuestionModel;
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

		Task<string> UpdateQuestionsAsync(int questionId, QuestionRequestModel questionRequestModel);
	}
}
