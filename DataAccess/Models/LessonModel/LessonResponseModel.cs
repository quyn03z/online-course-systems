using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.LessonModel
{
	public class LessonResponseModel
	{
		public int LessonId { get; set; }

		public string Title { get; set; }

		public int CourseId { get; set; }

		public bool IsLocked { get; set; }

		public string CourseName { get; set; }
	}
}
