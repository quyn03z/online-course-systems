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
	}
}
