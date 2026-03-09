using BusinessLogic.Claims;
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
		private readonly IClaimService _claimService;

		public SubLessonService(ISubLessonRepository subLessonRepository, IClaimService claimService)
		{
			_subLessonRepository = subLessonRepository;
			_claimService = claimService;
		}

		public async Task<SubLessonResponseModel> AddSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int lessonId)
		{
			var userId = _claimService.GetUserId();
			return await _subLessonRepository.AddSubLessonAsync(subLessonRequestModel, lessonId, userId.Value);
		}

		public async Task<List<SubLessonResponseModel>> GetAllsSubLesson(int lessonId)
		{
			return await _subLessonRepository.GetAllsSubLesson(lessonId);
		}

		public async Task<List<SubLessonResponseModel>> GetAllsSubLessonAsync(int lessonId)
		{
			return await _subLessonRepository.GetAllsSubLessonAsync(lessonId);
		}

		public async Task<int> GetFirstSubLessonByLessonId(int lessonId)
		{
			return await _subLessonRepository.GetFirstSubLessonByLessonId(lessonId);
		}

		public async Task<string> RemoveSubLessonAsync(int sublessonId)
		{
			var userId = _claimService.GetUserId();
			return await _subLessonRepository.RemoveSubLessonAsync(sublessonId,userId.Value);
		}

		public async Task<string> UpdateSubLessonAsync(SubLessonRequestModel subLessonRequestModel, int sublessonId)
		{
			var userId = _claimService.GetUserId();
			return await _subLessonRepository.UpdateSubLessonAsync(subLessonRequestModel, sublessonId,userId.Value);
		}
	}
}
