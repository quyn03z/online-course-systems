using DataAccess.Repositories.Impl;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class MenteeScoresRepository : BaseRepository<MenteeScores>, IMenteeScoresRepository
	{
		public MenteeScoresRepository(OCMSMSFContext context) : base(context)
		{
		}

		public async Task<MenteeScores?> FindByUserAndQuizAsync(int userId, int quizId)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.QuizId == quizId);
		}
	}
}
