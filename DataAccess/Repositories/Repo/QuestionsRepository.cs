using DataAccess.Models.AnswerModel;
using DataAccess.Models.QuestionModel;
using DataAccess.Repositories.Impl;
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

		public async Task<string> UpdateQuestionsAsync(int questionId, QuestionRequestModel questionRequestModel)
		{
			try
			{
				var parameters = new
				{
					questionId = questionId,
					QuizzId = questionRequestModel.QuizzId,
					QuestionText = questionRequestModel.QuestionText,
					TypeId = questionRequestModel.TypeId,
				};
				await _sqlDataAccess.ExecuteAsync("sp_UpdateQuestionsAsync", parameters);
				return "Cập nhật câu hỏi thành công.";
			}catch (Exception ex)
			{
				throw new Exception("Lỗi khi cập nhật câu hỏi.");
			}
		}
	}
}
