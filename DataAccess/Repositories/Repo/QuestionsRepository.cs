using DataAccess.Models.AnswerModel;
using DataAccess.Models.QuestionModel;
using DataAccess.Models.QuizzModel;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class QuestionsRepository : IQuestionsRepository
	{
		private readonly ISqlDataAccess _sqlDataAccess;

		public QuestionsRepository(ISqlDataAccess sqlDataAccess)
		{
			_sqlDataAccess = sqlDataAccess;
		}

		public async Task<QuestionResponseModel> AddQuestionsAsync(int quizzId, int userId, QuestionRequestModel questionRequestModel)
		{
			try
			{
				//Insert câu hỏi, lấy về QuestionId vừa được tạo
				int newQuestionId = await _sqlDataAccess.ExecuteSalarAsync<int>("sp_AddQuestions", new
				{
					QuizzId = quizzId,
					UserId = userId,
					QuestionText = questionRequestModel.QuestionText,
					TypeId = questionRequestModel.TypeId
				});

				//Insert từng đáp án với QuestionId vừa có
				var insertedAnswers = new List<AnswerResponseModel>();
				foreach (var answer in questionRequestModel.Answers ?? Enumerable.Empty<AnswerRequestModel>())
				{
					int newAnswerId = await _sqlDataAccess.ExecuteSalarAsync<int>("sp_AddAnswers", new
					{
						UserId = userId,
						QuestionId = newQuestionId,
						AnswerText = answer.AnswerText,
						IsCorrect = answer.IsCorrect
					});

					insertedAnswers.Add(new AnswerResponseModel
					{
						AnswerId = newAnswerId,
						QuestionId = newQuestionId,
						AnswerText = answer.AnswerText,
						IsCorrect = answer.IsCorrect ?? false
					});
				}

				//Trả về object QuestionResponseModel đầy đủ
				return new QuestionResponseModel
				{
					QuestionId = newQuestionId,
					QuizzId = quizzId,
					QuestionText = questionRequestModel.QuestionText,
					TypeId = questionRequestModel.TypeId,
					Answers = insertedAnswers
				};
			}
			catch (Exception ex)
			{
				throw new Exception("Thêm câu hỏi có lỗi.");
			}
		}

		public async Task<string> DeleteQuestionsAsync(int questionId, int userId)
		{
			try
			{
				await _sqlDataAccess.ExecuteAsync("sp_DeleteQuestionsAsync", new { questionId, userId});
				return "Xóa câu hỏi thành công.";
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi khi xóa câu hỏi.");
			}
		}

		public async Task<List<QuestionResponseModel>> GetAllsQuestionAsync(int quizzId)
		{
			try
			{
				var result = await _sqlDataAccess.QueryMultipleAsync(
					"sp_GetAllsQuestion",
					async (reader) =>
					{
						// Đọc kết quả bảng 1 (Questions)
						var questions = (await reader.ReadAsync<QuestionResponseModel>()).ToList();

						// Đọc kết quả bảng 2 (Answers)
						var answers = (await reader.ReadAsync<AnswerResponseModel>()).ToList();

						// Lặp qua từng câu hỏi và gán danh sách câu trả lời tương ứng
						foreach (var q in questions)
						{
							// Dùng LINQ để lấy các answer có QuestionId khớp với câu hỏi hiện tại
							q.Answers = answers.Where(a => a.QuestionId == q.QuestionId).ToList();
						}

						return questions;
					},
					new { QuizzId = quizzId }
				);

				return result;
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi khi lấy tất cả câu hỏi.");
			}
		}

		public async Task<string> UpdateQuestionsAsync(int questionId, int userId, QuizzQuestionsRequestModel quizzQuestionsRequestModel)
		{
			try
			{
				// Bước 1: Update Question (QuestionText, TypeId)
				await _sqlDataAccess.ExecuteAsync("sp_UpdateQuestionsAsync", new
				{
					questionId = questionId,
					QuizzId = quizzQuestionsRequestModel.QuizzId,
					QuestionText = quizzQuestionsRequestModel.QuestionText,
					TypeId = quizzQuestionsRequestModel.TypeId,
					UserId = userId
				});

				// Bước 2: Xóa toàn bộ answers cũ của câu hỏi này
				await _sqlDataAccess.ExecuteAsync("sp_DeleteAnswersByQuestionId", new
				{
					QuestionId = questionId,
					UserId = userId
				});

				// Bước 3: Insert lại toàn bộ answers mới
				foreach (var answer in quizzQuestionsRequestModel.Answers ?? Enumerable.Empty<AnswerRequestModel>())
				{
					await _sqlDataAccess.ExecuteSalarAsync<int>("sp_AddAnswers", new
					{
						UserId = userId,
						QuestionId = questionId,
						AnswerText = answer.AnswerText,
						IsCorrect = answer.IsCorrect
					});
				}

				return "Cập nhật câu hỏi thành công.";
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi khi cập nhật câu hỏi.");
			}
		}
	}
}
