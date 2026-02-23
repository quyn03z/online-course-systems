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
		public string? CourseTypeName { get; set; }
	}
}
