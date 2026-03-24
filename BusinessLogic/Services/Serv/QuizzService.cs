using BusinessLogic.Claims;
using BusinessLogic.Exceptions;
using BusinessLogic.Services.Impl;
using DataAccess.Models.QuizzModel;
using DataAccess.Repositories.Impl;
using DataAccess.Repositories.Repo;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class QuizzService : IQuizzService
	{
		private readonly IQuizzRepository _quizzRepository;
		private readonly IMenteeScoresRepository _menteeScoresRepository;

		public QuizzService(IQuizzRepository quizzRepository, IMenteeScoresRepository menteeScoresRepository)
		{
			_quizzRepository = quizzRepository;
			_menteeScoresRepository = menteeScoresRepository;
		}

		public async Task<QuizzResponseModel> AddQuizzAsync(QuizzRequestModel quizzRequestModel, int lessonId)
		{
			try
			{
				var addQuizz = new Quizz
				{
					Title = quizzRequestModel.Title,
					LessonId = lessonId,
					QuizzTime = quizzRequestModel.QuizzTime,
					IsDelete = false,
					IsLocked = quizzRequestModel.IsLocked,
				};
				await _quizzRepository.AddAsync(addQuizz);
				return new QuizzResponseModel
				{
					QuizzId = addQuizz.QuizzId,
					Title = addQuizz.Title,
					LessonId = addQuizz.LessonId,
					QuizzTime = addQuizz.QuizzTime,
					IsDelete = addQuizz.IsDelete,
					IsLocked = addQuizz.IsLocked
				};

			}catch (Exception ex)
			{
				throw new Exception("Mỗi Lesson chỉ có thể tạo một Quiz duy nhất.");
			}
		}

		public async Task<QuizzResponseModel> GetManaQuizzByLessonIdAsync(int lessonId)
		{
			var quizz = await _quizzRepository.GetManaQuizzByLessonIdAsync(lessonId);
			return new QuizzResponseModel
			{
				QuizzId = quizz.QuizzId,
				Title = quizz.Title,
				LessonId = quizz.LessonId,
				QuizzTime = quizz.QuizzTime,
				IsLocked= quizz.IsLocked
			};
		}

		public async Task<QuizzResponseModel> GetQuizzByLessonIdAsync(int lessonId)
		{
			var quizz = await _quizzRepository.GetQuizzByLessonIdAsync(lessonId);
			return new QuizzResponseModel
			{
				QuizzId = quizz.QuizzId,
				Title = quizz.Title,
				LessonId = quizz.LessonId,
				QuizzTime = quizz.QuizzTime,
			};
		}

		public async Task<string> RemoveQuizzAsync(int quizzId)
		{
			try
			{
				var quizz = await _quizzRepository.GetByIdAsync(quizzId);
				if (quizz == null) throw new BadRequestException("Quizz không tồn tại trong hệ thống!");

				await _menteeScoresRepository.RemoveQuizzIdAsync(quizzId);

				await _quizzRepository.DeleteAsync(quizz);
				return "Remove Quizz thành công";
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi xóa quizz.", ex);
			}
		}

		public async Task<string> UpdateQuizzAsync(QuizzRequestModel quizzRequestModel, int quizzId)
		{
			try
			{
				var quizz = await _quizzRepository.GetByIdAsync(quizzId);
				if (quizz == null) throw new BadRequestException("Quizz không tồn tại trong hệ thống!");

				quizz.Title = quizzRequestModel.Title;
				quizz.QuizzTime = quizzRequestModel.QuizzTime;
				quizz.IsLocked = quizzRequestModel.IsLocked;

				await _quizzRepository.UpdateAsync(quizz);

				return "Update Quizz thành công";
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi cập nhật quizz.", ex);
			}
		}





	}
}
