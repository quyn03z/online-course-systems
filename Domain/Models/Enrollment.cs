namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Enrollment")]
    public partial class Enrollment
    {
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public virtual Course Course { get; set; }

        public virtual User User { get; set; }
    }
}
