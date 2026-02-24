using DataAccess.Models.CourseModel;
using DataAccess.Models.LessonModel;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
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

		public async Task<List<SubLessonResponseModel>> GetAllsSubLessonAsync(int lessonId)
		{
			try
			{
				var allsLesson = await _sqlDataAccess.QueryAsync<SubLessonResponseModel>("sp_GetAllsSubLesson", new {lessonId = lessonId});
				return allsLesson.ToList();
			}
			catch (Exception ex)
			{
				throw new Exception("Lỗi lấy tất cả bài học.", ex);
			}
		}



	}
}
