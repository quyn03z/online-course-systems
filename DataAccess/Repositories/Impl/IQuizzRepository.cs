using DataAccess.Models.QuizzModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IQuizzRepository : IBaseRepository<Quizz>
	{
		Task<Quizz> GetQuizzByLessonIdAsync(int lessonId);
		Task<Quizz> GetManaQuizzByLessonIdAsync(int lessonId);
	}
}
