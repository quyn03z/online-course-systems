using BusinessLogic.Exceptions;
using BusinessLogic.Services.Impl;
using DataAccess.Models.QuizzModel;
using DataAccess.Repositories.Impl;
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

		public QuizzService(IQuizzRepository quizzRepository)
		{
			_quizzRepository = quizzRepository;
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
				};
				await _quizzRepository.AddAsync(addQuizz);
				return new QuizzResponseModel
				{
					QuizzId = addQuizz.QuizzId,
					Title = addQuizz.Title,
					LessonId = addQuizz.LessonId,
					QuizzTime = addQuizz.QuizzTime
				};

			}catch (Exception ex)
			{
				throw new Exception("Có lỗi khi thêm quizz mới.", ex);
			}
		}

		public async Task<IEnumerable<QuizzResponseModel>> GetAllQuizzAsync()
		{
			var allsQuizz = await _quizzRepository.GetAll();
			return allsQuizz.Select(q => new QuizzResponseModel
			{
				QuizzId    = q.QuizzId,
				Title      = q.Title,
				LessonId   = q.LessonId,
				QuizzTime = q.QuizzTime,
			}).ToList();
		}

		public async Task<string> RemoveQuizzAsync(int quizzId)
		{
			try
			{
				var quizz = await _quizzRepository.GetByIdAsync(quizzId);
				if (quizz == null) throw new BadRequestException("Quizz không tồn tại trong hệ thống!");

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
