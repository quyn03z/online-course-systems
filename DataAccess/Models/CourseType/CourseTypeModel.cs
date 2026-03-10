using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.CourseType
{
	public class CourseTypeModel
	{
		public int CourseTypeId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}

	public class CourseResponseTypeModel
	{
		public int CourseTypeId { get; set; }

		public string Name { get; set; }
	}
}
