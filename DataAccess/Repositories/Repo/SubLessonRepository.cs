using DataAccess.Models.CourseModel;
using DataAccess.Models.LessonModel;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class SubLessonRepository : ISubLessonRepository
	{
		private readonly ISqlDataAccess _sqlDataAccess;

		public SubLessonRepository(ISqlDataAccess sqlDataAccess)
		{
			_sqlDataAccess = sqlDataAccess;
		}

		public async Task<SubLessonResponseModel> AddSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int lessonId, int userId)
		{
			try
			{
				var parameters = new 
				{ 
					Title = subLessonRequestModel.Title,
					Content = subLessonRequestModel.Content,
					Description = subLessonRequestModel.Description,
					LessonId = lessonId,
					CreateDate = subLessonRequestModel?.CreateDate,
					IsLocked = subLessonRequestModel?.IsLocked,
					VideoLink = subLessonRequestModel?.VideoLink,
					UserId = userId
				};
				int newSubLessonId = await _sqlDataAccess.ExecuteSalarAsync<int>("sp_AddSubLesson", parameters);
				return new SubLessonResponseModel
				{
					SubLessonId = newSubLessonId,
					Title = subLessonRequestModel.Title,
					Content = subLessonRequestModel.Content,
					Description = subLessonRequestModel.Description,
					LessonId = lessonId,
					CreateDate = subLessonRequestModel?.CreateDate,
					IsLocked = subLessonRequestModel?.IsLocked,
					VideoLink = subLessonRequestModel?.VideoLink,
				};

			}catch (Exception ex)
			{
				throw new Exception("Lỗi khi thêm bài học.", ex);
			}
		}

		public async Task<List<SubLessonResponseModel>> GetAllsSubLesson(int lessonId)
		{
			try
			{
				var allsLesson = await _sqlDataAccess.QueryAsync<SubLessonResponseModel>("sp_GetAllsSubLesson", new { lessonId = lessonId });
				return allsLesson.ToList();
			} catch (Exception ex)
			{
				throw new Exception("Lỗi lấy tất cả bài học.", ex);

			}
		}

		public async Task<List<SubLessonResponseModel>> GetAllsSubLessonAsync(int lessonId)
		{
			try
			{
				var allsLesson = await _sqlDataAccess.QueryAsync<SubLessonResponseModel>("GetAllsSubLessonAsync", new {lessonId = lessonId});
				return allsLesson.ToList();
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi lấy tất cả bài học.", ex);
			}
		}

		public async Task<int> GetFirstSubLessonByLessonId(int lessonId)
		{
			try
			{
				var subLessonId = await _sqlDataAccess.QueryFirstOrDefaultAsync<int>("GetFirstSubLessonByLessonId", new { lessonId = lessonId });
				return subLessonId;
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi lấy tất cả bài học.", ex);
			}
		}

		public async Task<string> RemoveSubLessonAsync(int subLessonId, int userId)
		{
			try
			{
				await _sqlDataAccess.ExecuteAsync("sp_RemoveSubLesson", new {SubLessonId = subLessonId,UserId = userId});
				return "Remove sublesson thành công.";
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi xóa bài học.", ex);
			}
		}

		public async Task<string> UpdateSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int sublessonId, int userId)
		{
			try
			{
				await _sqlDataAccess.ExecuteAsync("sp_UpdateSubLesson", new
				{
					SubLessonId = sublessonId,
					Title = subLessonRequestModel.Title,
					Content = subLessonRequestModel.Content,
					Description = subLessonRequestModel.Description,
					IsLocked = subLessonRequestModel.IsLocked,
					VideoLink = subLessonRequestModel.VideoLink,
					UserId = userId
				});
				return "Cập nhật sublesson thành công.";
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi update bài học.", ex);
			}
		}
	}
}
