namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class MenteeScores
    {
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuizId { get; set; }

        public double? Score { get; set; }

        public virtual Quizz Quizz { get; set; }

        public virtual User User { get; set; }
    }
}
