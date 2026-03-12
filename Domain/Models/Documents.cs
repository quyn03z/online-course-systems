using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	[Table("Documents")]
	public class Documents
	{
		[Key]
		public int DocumentId { get; set; }

		[Required]
		[StringLength(255)]
		public string Title { get; set; }

		public string? Description { get; set; }

		public string FileUrl { get; set; }

		public int LessonId { get; set; }

		public DateTime? CreateDate { get; set; }

		public bool? IsLocked { get; set; }
		public bool? IsDelete { get; set; }

		public virtual Lesson Lesson { get; set; }

	}
}
