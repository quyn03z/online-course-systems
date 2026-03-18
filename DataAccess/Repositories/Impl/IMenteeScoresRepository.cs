using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IMenteeScoresRepository : IBaseRepository<MenteeScores>
	{
		Task<MenteeScores?> FindByUserAndQuizAsync(int userId, int quizId);
	}
}
