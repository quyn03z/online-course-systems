using DataAccess.Models.CourseModel;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Impl;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class CourseRepository : ICourseRepository
	{
		private readonly IDbConnectionFactory _connectionFactory;

		public CourseRepository(IDbConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<List<CourseResponseModel>> GetAllCourseAsync()
		{
			var allsCourse = new List<CourseResponseModel>();
			try
			{
				// b1 mở connect đến Db
				using var conn = _connectionFactory.CreateConnection();
				// b2 dùng sql command để thao tác với database
				using var cmd = new SqlCommand("sp_GetAllCourse", conn);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;

				// Mở kết nối trước khi đọc
				await conn.OpenAsync();

				// b3 dùng sql reader để đọc dữ liệu từ command
				using var reader = await cmd.ExecuteReaderAsync();

				while (await reader.ReadAsync())
				{
					allsCourse.Add(new CourseResponseModel
					{
						CourseId       = reader.GetInt32("CourseId"),
						CourseName     = reader.GetString("CourseName").Trim(),
						Title          = reader.GetString("Title").Trim(),
						Description    = reader.GetString("Description").Trim(),
						Image          = reader.GetString("Image").Trim(),
						IsLocked       = reader.GetBoolean("IsLocked"),
						IsDelete       = reader.GetBoolean("IsDelete"),
						Price          = Convert.ToDouble(reader["Price"]),
						CourseTypeName = reader.GetString("CourseTypeName").Trim(),
					});
				}
			} catch (Exception ex)
			{
				throw new Exception("Lỗi lấy tất cả khóa học.", ex);
			}

			return allsCourse;
		}


	}
}
