using BusinessLogic.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Teacher
{
	public class ManaQuestionsController : BaseController
	{
		private readonly IQuestionsService _questionsService;

		public ManaQuestionsController(IQuestionsService questionsService)
		{
			_questionsService = questionsService;
		}
	}
}
