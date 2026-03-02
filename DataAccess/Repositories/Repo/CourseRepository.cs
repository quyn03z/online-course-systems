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
		private readonly ISqlDataAccess _sqlDataAccess;

		public CourseRepository(ISqlDataAccess sqlDataAccess)
		{
			_sqlDataAccess = sqlDataAccess;
		}

		public async Task<CourseResponseModel> AddCourseAsync(CourseRequestModel courseRequestModel)
		{
			try
			{
				int newCourseId = await _sqlDataAccess.ExecuteSalarAsync<int>("sp_AddCourse", courseRequestModel);
				return new CourseResponseModel 
				{
					CourseId = newCourseId,
					CourseName = courseRequestModel.CourseName,
					Title = courseRequestModel.Title,
					Description = courseRequestModel.Description,
					Image = courseRequestModel.Image,
					IsLocked = courseRequestModel.IsLocked,
					IsDelete = courseRequestModel.IsDelete,
					Price = courseRequestModel.Price,
					CourseTypeId = courseRequestModel.CourseTypeId,
				};
			}
			catch (Exception ex)
			{
				throw new Exception("Thêm Khóa học thất bại.", ex);
			}
		}

		public async Task<List<CourseResponseModel>> GetAllHomeCourseAsync()
		{
			try
			{
				var allsHomeCourse = await _sqlDataAccess.QueryAsync<CourseResponseModel>("sp_GetAllHomeCourseAsync", null);
				return allsHomeCourse.ToList();
			} catch (Exception ex)
			{
				throw new Exception("Lỗi lấy tất cả khóa học.",ex);
			}
		}

		public async Task<List<CourseResponseModel>> GetAllManaCourseAsync()
		{
			try
			{
				var allsManaCourse = await _sqlDataAccess.QueryAsync<CourseResponseModel>("sp_GetAllManaCourseAsync", null);
				return allsManaCourse.ToList();
			}
			catch (Exception ex)
			{
				throw new Exception("Không có khóa học tương ứng.", ex);
			}
		}

		public async Task<CourseResponseModel> GetCourseById(int courseId)
		{
			try
			{
				return await _sqlDataAccess.QueryFirstOrDefaultAsync<CourseResponseModel>("sp_GetCourseById", new { CourseId = courseId });

			}catch (Exception ex)
			{
				throw new Exception("Không có khóa học tương ứng.",ex);
			}
		}

		public async Task<string> RemoveCourseById(int courseId)
		{
			try
			{
				await _sqlDataAccess.ExecuteAsync("sp_RemoveCourseById", new { CourseId = courseId });
				return "Xóa khóa học thành công.";
			}
			catch (Exception ex)
			{
				throw new Exception("Xóa khóa học thất bại.", ex);
			}
		}

		public async Task<string> UpdateCourseAsync( CourseRequestModel courseRequestModel, int courseId)
		{
			try
			{
				await _sqlDataAccess.ExecuteAsync("sp_UpdateCourse", new
				{
					CourseId     = courseId,
					CourseName   = courseRequestModel.CourseName,
					Title        = courseRequestModel.Title,
					Description  = courseRequestModel.Description,
					Image        = courseRequestModel.Image,
					IsLocked       = courseRequestModel.IsLocked,
					IsDelete     = courseRequestModel.IsDelete,
					Price        = courseRequestModel.Price,
					CourseTypeId = courseRequestModel.CourseTypeId,
				});
				return "Cập nhật khóa học thành công.";
			}
			catch (Exception ex)
			{
				throw new Exception("Cập nhật khóa học thất bại.", ex);
			}
		}

		

	}
}
