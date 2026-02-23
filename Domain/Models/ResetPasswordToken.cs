namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ResetPasswordToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(256)]
        public string ResetToken { get; set; }

        public DateTime ExpiredAt { get; set; }

        public bool isUsed { get; set; }

        public DateTime CreateAt { get; set; }

        public virtual User User { get; set; }
    }
}
