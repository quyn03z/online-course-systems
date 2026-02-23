namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class QuestionType
    {
        public QuestionType()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        public int TypeId { get; set; }

        [StringLength(250)]
        public string TypeName { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
