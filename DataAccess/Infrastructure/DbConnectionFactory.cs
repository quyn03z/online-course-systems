using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Infrastructure
{
	public class DbConnectionFactory : IDbConnectionFactory
	{
		private readonly string _connectionString;

		public DbConnectionFactory(IConfiguration configuration)
		{
			_connectionString = configuration["Database:ConnectionString"];
		}

		public SqlConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
