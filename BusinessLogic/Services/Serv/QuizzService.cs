using BusinessLogic.Services.Impl;
using DataAccess.Models.QuizzModel;
using DataAccess.Repositories.Impl;
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


	}
}
