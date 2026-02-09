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
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected readonly OCMSMSFContext _context;
		protected readonly DbSet<T> _dbSet;

		public BaseRepository(OCMSMSFContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public async Task<T> AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<T> UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}


	}
}
