namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SubLesson")]
    public partial class SubLesson
    {
        [Key]
        public int SubLessonId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public int LessonId { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? IsLocked { get; set; }
		public bool? IsDelete { get; set; }


		public string VideoLink { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
