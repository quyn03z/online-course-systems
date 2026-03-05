using Dapper;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Impl;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

		public async Task<T> ExecuteQuerySingleAsync<T>(string storedProcedure, object parameters = null)
		{
			using var conn = _dbConnectionFactory.CreateConnection();
			return await conn.QuerySingleOrDefaultAsync<T>(
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

		public async Task<T> QueryMultipleAsync<T>(string storedProcedure, Func<SqlMapper.GridReader, Task<T>> mapFunc, object parameters = null)
		{
			using var conn = _dbConnectionFactory.CreateConnection();
			using (var multi = await conn.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure))
			{
				return await mapFunc(multi);
			}
		}


	}
}
