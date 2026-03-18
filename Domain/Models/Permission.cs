using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Permission
	{
		public int Id { get; set; }
		public string Name { get; set; }          // "create_lesson"
		public string DisplayName { get; set; }   // "Tạo bài học"
		public string Group { get; set; }          // "Lesson", "Course", "User"
	}
}
