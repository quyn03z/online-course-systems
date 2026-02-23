namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class RefreshToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Token { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime ExpiredAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
