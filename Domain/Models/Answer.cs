namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        public int? QuestionId { get; set; }

        [Column(TypeName = "text")]
        public string AnswerText { get; set; }

        public bool? IsCorrect { get; set; }

        public virtual Question Question { get; set; }
    }
}
