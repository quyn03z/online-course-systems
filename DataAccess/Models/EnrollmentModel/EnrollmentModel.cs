using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Enrollment
{
	public class EnrollmentModel
	{
		public int UserId { get; set; }

		public int CourseId { get; set; }

		public string IpAddress { get; set; }
		public string UserAgent { get; set; }
		public int DurationMs { get; set; }
	}

	public class PurchaseHistoryModel
	{
		public int CourseId { get; set; }
		public string CourseName { get; set; }
		public string Image { get; set; }

	}

	public class TopCourseModel
	{
		public List<string> Labels { get; set; } = new List<string>();
		public List<decimal> Data { get; set; } = new List<decimal>();
	}
	public class CourseRevenue
	{
		public string CourseName { get; set; }
		public int TotalEnrollment { get; set; }
	}

}
