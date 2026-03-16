using BusinessLogic.Claims;
using BusinessLogic.Services.Impl;
using DataAccess.Models.QuestionModel;
using DataAccess.Models.QuizzModel;
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
		private readonly IClaimService _claimService;

		public QuestionsService(IQuestionsRepository questionsRepository, IClaimService claimService)
		{
			_questionsRepository = questionsRepository;
			_claimService = claimService;
		}

		public async Task<QuestionResponseModel> AddQuestionsAsync(int quizzId, QuestionRequestModel questionRequestModel)
		{
			var userId = _claimService.GetUserId();
			return await _questionsRepository.AddQuestionsAsync(quizzId, userId.Value ,questionRequestModel);
		}

		public async Task<string> DeleteQuestionsAsync(int questionId)
		{
			var userId = _claimService.GetUserId();
			return await _questionsRepository.DeleteQuestionsAsync(questionId, userId.Value);
		}

		public async Task<List<QuestionResponseModel>> GetAllsQuestionAsync(int quizzId)
		{
			return await _questionsRepository.GetAllsQuestionAsync(quizzId);
		}

		public async Task<string> UpdateQuestionsAsync(int questionId, QuizzQuestionsRequestModel quizzQuestionsRequestModel)
		{
			var userId = _claimService.GetUserId();
			return await _questionsRepository.UpdateQuestionsAsync(questionId, userId.Value,quizzQuestionsRequestModel);
		}
	}
}
