using DataAccess.Models.MenteeScoreModel;
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

		public async Task<CheckProgressModel> CheckProgressAsync(int courseId, int userId)
		{
			var totalQuizzes = await _context.Lessons
				.Where(l => l.CourseId == courseId && l.Quizz != null && (l.IsDelete == false || l.IsDelete == null))
				.CountAsync();

			var completedQuizzes = await _context.MenteeScores
				.Where(ms => ms.UserId == userId && _context.Quizzs.Any(q => q.QuizzId == ms.QuizId && q.Lesson.CourseId == courseId) && ms.Score >= 8)
				.CountAsync();

			double percentage = totalQuizzes > 0 ? (double)completedQuizzes / totalQuizzes * 100 : 0;
			if (percentage > 100) percentage = 100;

			return new CheckProgressModel
			{
				CourseId = courseId,
				Progress = $"{Math.Round(percentage, 2)}%",
				IsCompleted = percentage >= 100
			};
		}

		public async Task<MenteeScores?> FindByUserAndQuizAsync(int userId, int quizId)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.QuizId == quizId);
		}

		public async Task RemoveQuizzIdAsync(int quizzId)
		{
			var menteeScores = await _context.MenteeScores
				   .Where(ms => ms.QuizId == quizzId)
				   .ToListAsync();
			
			if (menteeScores.Any())
			{
				_context.MenteeScores.RemoveRange(menteeScores);
				await _context.SaveChangesAsync();
			}
		}


	}
}
