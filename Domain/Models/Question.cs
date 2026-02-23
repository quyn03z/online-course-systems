namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        [Key]
        public int QuestionId { get; set; }

        public int? QuizId { get; set; }

        [Column(TypeName = "text")]
        public string QuestionText { get; set; }

        public int? TypeId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual QuestionType QuestionType { get; set; }

        public virtual Quizz Quizz { get; set; }
    }
}
