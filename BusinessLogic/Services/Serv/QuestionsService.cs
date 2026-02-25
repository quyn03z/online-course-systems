using BusinessLogic.Services.Impl;
using DataAccess.Models.QuestionModel;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class QuestionsService : IQuestionsService
	{
		private readonly IQuestionsRepository _questionsRepository;

		public QuestionsService(IQuestionsRepository questionsRepository)
		{
			_questionsRepository = questionsRepository;
		}

		public async Task<List<QuestionResponseModel>> GetAllsQuestionAsync(int quizzId)
		{
			return await _questionsRepository.GetAllsQuestionAsync(quizzId);
		}

		public async Task<string> UpdateQuestionsAsync(int questionId, QuestionRequestModel questionRequestModel)
		{
			return await _questionsRepository.UpdateQuestionsAsync(questionId, questionRequestModel);
		}
	}
}
