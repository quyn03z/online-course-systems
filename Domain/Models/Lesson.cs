namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Lesson")]
    public partial class Lesson
    {
        public Lesson()
        {
            SubLessons = new HashSet<SubLesson>();
        }

        public int LessonId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual Quizz Quizz { get; set; }

        public virtual ICollection<SubLesson> SubLessons { get; set; }
    }
}
