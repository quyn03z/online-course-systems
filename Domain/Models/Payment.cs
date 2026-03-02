namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int? UserId { get; set; }

        public int? CourseId { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string TransactionCode { get; set; }

        public virtual Course Course { get; set; }

        public virtual User User { get; set; }
    }
}
