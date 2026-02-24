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

		public async Task<List<SubLessonResponseModel>> GetAllsSubLessonAsync(int lessonId)
		{
			return await _subLessonRepository.GetAllsSubLessonAsync(lessonId);
		}
	}
}
