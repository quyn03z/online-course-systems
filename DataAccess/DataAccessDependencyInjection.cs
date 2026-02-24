using DataAccess.Infrastructure;
using DataAccess.Repositories;
using DataAccess.Repositories.Impl;
using DataAccess.Repositories.Repo;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public static class DataAccessDependencyInjection
	{
		public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDatabase(configuration); // 1. Gọi hàm cấu hình DB

			services.AddRepositories();          // 2. Gọi hàm đăng ký Repository

			return services;
		}

		private static void AddRepositories(this IServiceCollection services)
		{
			// sql data access
			services.AddScoped<ISqlDataAccess, SqlDataAccess>();

			// connection factory
			services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

			// user
			services.AddScoped<IUserRepository, UserRepository>();

			// course
			services.AddScoped<ICourseRepository, CourseRepository>();

			// lesson
			services.AddScoped <ILessonRepository,LessonRepository>();

			// role
			services.AddScoped<IRoleRepository, RoleRepository>();

			// refreshToken
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

			// resetPasswordToken
			services.AddScoped<IResetPasswordTokenRepository, ResetPasswordTokenRepository>();

		}

		private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			var databaseConfig = configuration.GetSection("Database").Get<DatabaseConfiguration>();
			if (databaseConfig != null && databaseConfig.UseInMemoryDatabase)
			{
				services.AddDbContext<OCMSMSFContext>(options =>
					options.UseInMemoryDatabase("OnlineCourseMSF"));
			}
			else
			{
				var connectionString = databaseConfig?.ConnectionString ?? string.Empty;
				services.AddDbContext<OCMSMSFContext>(options =>
					options.UseSqlServer(connectionString,
						opt => opt.MigrationsAssembly(typeof(OCMSMSFContext).Assembly.FullName)));
			}
		}


		private class DatabaseConfiguration
		{
			public bool UseInMemoryDatabase { get; set; }
			public string? ConnectionString { get; set; }
		}
	}
}
