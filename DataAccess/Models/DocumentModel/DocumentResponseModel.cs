using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.DocumentModel
{
	public class DocumentResponseModel
	{
		public int DocumentId { get; set; }

		public string Title { get; set; }
		public string? Description { get; set; }
		public string FileUrl { get; set; }
		public int LessonId { get; set; }
		public bool? IsLocked { get; set; }
	}


	public class DocumentRequestModel
	{
		[Required(ErrorMessage = "Title là bắt buộc.")]
		[StringLength(150, MinimumLength = 3, ErrorMessage = "Title phải từ 3 đến 150 ký tự.")]
		public string Title { get; set; }
		public string? Description { get; set; }
		[Required(ErrorMessage = "FileUrl là bắt buộc.")]
		public string FileUrl { get; set; }
		public int LessonId { get; set; }
		public bool? IsLocked { get; set; }
	}
}
