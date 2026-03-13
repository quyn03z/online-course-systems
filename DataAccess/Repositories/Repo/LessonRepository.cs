using DataAccess.Models.LessonModel;
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
	public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
	{
		public LessonRepository(OCMSMSFContext context) : base(context)
		{
		}

		public async Task<IEnumerable<Lesson>> GetAllLessonAsync(int courseId)
		{
			return await _dbSet.Include(c => c.Course).Where(c => c.CourseId == courseId && c.IsLocked == false && c.IsDelete == false).ToListAsync();
		}

		public async Task<IEnumerable<Lesson>> GetAllManaLessonAsync(int courseId)
		{
			return await _dbSet.Include(c => c.Course).Where(l => l.CourseId == courseId && l.IsDelete == false).ToListAsync();
		}

		public async Task<int> GetFirstLessonIdByCourseId(int courseId)
		{
			return await _dbSet
						.Where(l => l.CourseId == courseId)
						.Select(l => l.LessonId).FirstOrDefaultAsync();
		}

	}

}
