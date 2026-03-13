using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.CourseModel
{
	public class CourseResponseModel
	{
		public int CourseId { get; set; }
		public string CourseName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public bool? IsLocked { get; set; }
		public bool? IsDelete { get; set; }
		public double Price { get; set; }
		public int? CourseTypeId { get; set; }
		public string? CourseTypeName { get; set; }
	}

	public class CourseResponseHomeModel
	{
		public int CourseId { get; set; }
		public string CourseName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public double Price { get; set; }
		public string Creator { get; set; }
		public string? CourseTypeName { get; set; }
	}


	public class CourseRequestModel
	{
		[Required(ErrorMessage = "Tên khóa học là bắt buộc.")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = "Tên khóa học phải từ 3 đến 50 ký tự.")]
		public string CourseName { get; set; }
		[Required(ErrorMessage = "Tên tiêu đề là bắt buộc.")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = "Tên tiêu đề  phải từ 3 đến 50 ký tự.")]
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public bool? IsLocked { get; set; }
		public bool? IsDelete { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0.")]
		public double Price { get; set; }
		public int? CourseTypeId { get; set; }
	}
}
