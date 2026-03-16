using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.QuizzModel
{
	public class QuizzResponseModel
	{
		public int QuizzId { get; set; }

		public string Title { get; set; }

		public int LessonId { get; set; }

		public int QuizzTime { get; set; }
		public bool? IsLocked { get; set; }
		public bool? IsDelete { get; set; }
	}
}
