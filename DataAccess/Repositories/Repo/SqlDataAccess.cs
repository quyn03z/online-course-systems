using Dapper;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class SqlDataAccess : ISqlDataAccess
	{
		private readonly IDbConnectionFactory _dbConnectionFactory;

		public SqlDataAccess(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory;
		}

		public async Task<int> ExecuteAsync(string storedProcedure, object parameters = null)
		{
			using var conn = _dbConnectionFactory.CreateConnection();
			return await conn.ExecuteAsync(
								storedProcedure,
								parameters,
								commandType: CommandType.StoredProcedure);
		}

		public async Task<T> ExecuteSalarAsync<T>(string storedProcedure, object parameters = null)
		{
			using var conn = _dbConnectionFactory.CreateConnection();
			return await conn.ExecuteScalarAsync<T>(
						storedProcedure,
						parameters,
						commandType: CommandType.StoredProcedure);
		}

		public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object parameters = null)
		{
			using var conn = _dbConnectionFactory.CreateConnection();
			return await conn.QueryAsync<T>(
						storedProcedure,
						parameters,
						commandType: CommandType.StoredProcedure);
		}

		public async Task<T> QueryFirstOrDefaultAsync<T>(string storedProcedure, object parameters = null)
		{
			using var conn = _dbConnectionFactory.CreateConnection();
			return await conn.QueryFirstOrDefaultAsync<T>(
						storedProcedure,
						parameters,
						commandType: CommandType.StoredProcedure);
		}
	}
}
