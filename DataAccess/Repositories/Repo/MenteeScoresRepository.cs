using DataAccess.Models.MenteeScoreModel;
using DataAccess.Models.UserCourse;
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

		public async Task<UserStatisticModel> GetUserStatisticByIdAsync(int userId)
		{
			var userScores = await _dbSet
				.Where(x => x.UserId == userId)
				.Include(x => x.Quizz)
				.OrderBy(x => x.QuizId) // Đảm bảo thứ tự cho biểu đồ
				.ToListAsync();

			if (!userScores.Any())
			{
				return new UserStatisticModel
				{
					AvgScore = 0,
					TotalAttempts = 0,
					MaxScore = 0,
					MinScore = 0,
					ChartStatistic = new List<CourseScoreUser>()
				};
			}

			return new UserStatisticModel
			{
				AvgScore = Math.Round(userScores.Average(x => x.Score) ?? 0, 2),
				TotalAttempts = userScores.Count,
				MaxScore = userScores.Max(x => x.Score) ?? 0,
				MinScore = userScores.Min(x => x.Score) ?? 0,
				ChartStatistic = userScores.Select(x => new CourseScoreUser
				{
					QuizzName = x.Quizz?.Title ?? "N/A",
					Score = x.Score
				}).ToList()
			};
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
