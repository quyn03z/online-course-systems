using BusinessLogic.Exceptions;
using BusinessLogic.Services.Impl;
using DataAccess.Models.LessonModel;
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
	public class LessonService : ILessonService
	{
		private readonly ILessonRepository _lessonRepository;

		public LessonService(ILessonRepository lessonRepository)
		{
			_lessonRepository = lessonRepository;
		}

		public async Task<LessonResponseModel> AddManaLessonAsync(LessonRequestModel lessonRequesModel, int courseId)
		{
			try
			{
				var lesson = new Lesson
				{
					CourseId = courseId,
					Title = lessonRequesModel.Title,
					IsLocked = lessonRequesModel.IsLocked,
					IsDelete = false
				};
				await _lessonRepository.AddAsync(lesson);

				return new LessonResponseModel { 
					LessonId = lesson.LessonId,
					Title = lesson.Title,
					IsLocked = lesson.IsLocked,
					CourseId = lesson.CourseId,
					IsDelete = lesson.IsDelete
				};
			}catch (Exception ex)
			{
				throw new Exception("Có lỗi khi thêm bài học mới.", ex);
			}
		}

		public async Task<IEnumerable<LessonResponseModel>> GetAllLessonAsync(int courseId)
		{
			try
			{
				var allsLesson = await _lessonRepository.GetAllLessonAsync(courseId);
				return allsLesson.Select(x => new LessonResponseModel
				{
					LessonId = x.LessonId,
					Title = x.Title,
					CourseName = x.Course.CourseName,
					IsLocked = x.IsLocked,
					CourseId = x.CourseId,
				});
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả bài học.", ex);
			}
		}

		public async Task<IEnumerable<LessonResponseModel>> GetAllManaLessonAsync(int courseId)
		{
			try
			{
				var allsLesson = await _lessonRepository.GetAllManaLessonAsync(courseId);
				return allsLesson.Select(x => new LessonResponseModel
				{
					LessonId = x.LessonId,
					Title = x.Title,
					CourseName = x.Course.CourseName,
					IsLocked = x.IsLocked,
					CourseId = x.CourseId,
				});
			}catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả bài học.",ex);
			}
		}

		public async Task<int> GetFirstLessonIdByCourseId(int courseId)
		{
			try
			{
				return await _lessonRepository.GetFirstLessonIdByCourseId(courseId);
			}catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy GetFirstLessonIdByCourseId.");
			}
		}

		public async Task<bool> GetLessonsById(int lessonId)
		{
			var lesson = await _lessonRepository.GetByIdAsync(lessonId);
			if (lesson != null)
			{
				return true;
			}
			return false;
		}

		public async Task<string> RemoveLessonAsync(int lessonId)
		{
			var lesson = await _lessonRepository.GetByIdAsync(lessonId);
			if (lesson == null) throw new BadRequestException("Lesson không tồn tại trong hệ thống!");

			lesson.IsDelete = true;
			lesson.IsLocked = true;
			await _lessonRepository.UpdateAsync(lesson);
			return "Remove Lesson Thành Công.";

		}

		public async Task<LessonResponseModel> UpdateManaLessonAsync(int lessonId, LessonRequestModel lessonRequesModel)
		{
			try
			{
				var lesson = await _lessonRepository.GetByIdAsync(lessonId);
				if (lesson == null) throw new BadRequestException("Lesson không tồn tại trong hệ thống!");

				lesson.Title    = lessonRequesModel.Title;
				lesson.IsLocked = lessonRequesModel.IsLocked;

				await _lessonRepository.UpdateAsync(lesson);

				return new LessonResponseModel
				{
					LessonId = lesson.LessonId,
					Title    = lesson.Title,
					IsLocked = lesson.IsLocked,
					CourseId = lesson.CourseId,
					IsDelete = lesson.IsDelete,
				};
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi cập nhật bài học.", ex);
			}
		}



	}
}
