using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.LessonModel
{
	public class SubLessonResponseModel
	{
		public int SubLessonId { get; set; }
		public string Title { get; set; }

		public string Content { get; set; }

		public string Description { get; set; }

		public int LessonId { get; set; }

		public DateTime? CreateDate { get; set; }

		public bool? IsLocked { get; set; }
		public bool? IsDelete{ get; set; }


		public string VideoLink { get; set; }
	}
}
