using BusinessLogic.Services.Impl;
using DataAccess.Repositories.Impl;
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



	}
}
