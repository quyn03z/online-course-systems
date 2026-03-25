using BusinessLogic.Claims;
using BusinessLogic.Services.Impl;
using DataAccess.Models.MenteeScoreModel;
using DataAccess.Models.UserCourse;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	
	public class MenteeScoreService : IMenteeScoreService
	{
		private readonly IMenteeScoresRepository _menteeScoresRepository;
		private readonly IClaimService _claimService;

		public MenteeScoreService(IMenteeScoresRepository menteeScoresRepository, IClaimService claimService)
		{
			_menteeScoresRepository = menteeScoresRepository;
			_claimService = claimService;
		}

		public async Task<MenteeScoreRequestModel> AddMenteeScoreAsync(MenteeScoreRequestModel menteeScoreRequestModel)
		{
			var userId = _claimService.GetUserId();

			// Kiểm tra đã có điểm cho quiz này chưa
			var existing = await _menteeScoresRepository.FindByUserAndQuizAsync(userId.Value, menteeScoreRequestModel.QuizId);

			if (existing != null)
			{
				// Nếu đã có thì update điểm mới (điểm cao nhất hoặc điểm mới nhất tùy ý)
				existing.Score = menteeScoreRequestModel.Score;
				await _menteeScoresRepository.UpdateAsync(existing);
			}
			else
			{
				// Nếu chưa có thì tạo mới
				var menteeScore = new MenteeScores
				{
					QuizId = menteeScoreRequestModel.QuizId,
					UserId = userId.Value,
					Score = menteeScoreRequestModel.Score,
				};
				await _menteeScoresRepository.AddAsync(menteeScore);
			}

			return new MenteeScoreRequestModel
			{
				Score = menteeScoreRequestModel.Score,
			};
		}

		public async Task<CheckProgressModel> GetProgressAsync(int courseId)
		{
			var userId = _claimService.GetUserId();
			return await _menteeScoresRepository.CheckProgressAsync(courseId, userId.Value);
		}

		public async Task<UserStatisticModel> GetUserStatisticByIdAsync(int userId)
		{
			return await _menteeScoresRepository.GetUserStatisticByIdAsync(userId);
		}
	}
}
