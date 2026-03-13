namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Quizz")]
    public partial class Quizz
    {
        public Quizz()
        {
            MenteeScores = new HashSet<MenteeScores>();
            Questions = new HashSet<Question>();
        }

        public int QuizzId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int LessonId { get; set; }

        public int QuizzTime { get; set; }
		public bool? IsDelete { get; set; }
		public bool? IsLocked { get; set; }


		public virtual Lesson Lesson { get; set; }

        public virtual ICollection<MenteeScores> MenteeScores { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
