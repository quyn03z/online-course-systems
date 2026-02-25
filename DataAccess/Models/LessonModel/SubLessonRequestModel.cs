using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.LessonModel
{
	public class SubLessonRequestModel
	{
		[Required(ErrorMessage = "Tên là bắt buộc.")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Tên phải từ 3 đến 50 ký tự.")]
		public string Title { get; set; }
		[Required(ErrorMessage = "Nội dung là bắt buộc.")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Nội dung phải từ 3 đến 50 ký tự.")]
		public string Content { get; set; }

		public string Description { get; set; }

		public DateTime? CreateDate { get; set; }

		public bool? IsLocked { get; set; }

		[Required(ErrorMessage = "Video là bắt buộc.")]
		public string VideoLink { get; set; }
	}
}
