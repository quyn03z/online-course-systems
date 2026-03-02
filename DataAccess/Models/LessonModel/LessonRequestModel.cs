using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.LessonModel
{
	public class LessonRequestModel
	{
		[Required(ErrorMessage = "Tên bài học là bắt buộc.")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Tên bài học phải từ 3 đến 50 ký tự.")]
		public string Title { get; set; }

		public bool IsLocked { get; set; }

	}
}
