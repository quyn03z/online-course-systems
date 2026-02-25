using BusinessLogic.Services.Impl;
using DataAccess.Models.LessonModel;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class SubLessonService : ISubLessonService
	{
		private readonly ISubLessonRepository _subLessonRepository;
		public SubLessonService(ISubLessonRepository subLessonRepository)
		{
			_subLessonRepository = subLessonRepository;
		}

		public async Task<SubLessonResponseModel> AddSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int lessonId)
		{
			return await _subLessonRepository.AddSubLessonAsync(subLessonRequestModel, lessonId);
		}

		public async Task<List<SubLessonResponseModel>> GetAllsSubLesson(int lessonId)
		{
			return await _subLessonRepository.GetAllsSubLesson(lessonId);
		}

		public async Task<List<SubLessonResponseModel>> GetAllsSubLessonAsync(int lessonId)
		{
			return await _subLessonRepository.GetAllsSubLessonAsync(lessonId);
		}

		public async Task<string> RemoveSubLessonAsync(int sublessonId)
		{
			return await _subLessonRepository.RemoveSubLessonAsync(sublessonId);
		}

		public async Task<string> UpdateSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int sublessonId)
		{
			return await _subLessonRepository.UpdateSubLessonAsync(subLessonRequestModel, sublessonId);
		}
	}
}
