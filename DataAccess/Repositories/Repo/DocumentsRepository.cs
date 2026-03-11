using DataAccess.Models.LessonModel;
using DataAccess.Repositories.Impl;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class DocumentsRepository : BaseRepository<Documents>, IDocumentsRepository
	{
		public DocumentsRepository(OCMSMSFContext context) : base(context)
		{
		}

		public async Task<List<Documents>> GetAllsDocuments(int lessonId)
		{
			return await _dbSet.Where(x => x.LessonId == lessonId && x.IsLocked == false).ToListAsync();
		}

		public async Task<List<Documents>> GetAllsManaDocumentsAsync(int lessonId)
		{
			return await _dbSet.Where(x => x.LessonId == lessonId).ToListAsync();
		}
	}
}
