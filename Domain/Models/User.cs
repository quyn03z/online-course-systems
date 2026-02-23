namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            AuditLogs = new HashSet<AuditLog>();
            Enrollments = new HashSet<Enrollment>();
            Mentee_scores = new HashSet<MenteeScores>();
            Payments = new HashSet<Payment>();
            RefreshTokens = new HashSet<RefreshToken>();
            ResetPasswordTokens = new HashSet<ResetPasswordToken>();
        }

        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }

        [StringLength(100)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public bool IsLocked { get; set; }

        public int RoleId { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<MenteeScores> Mentee_scores { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<ResetPasswordToken> ResetPasswordTokens { get; set; }
        public virtual Role Role { get; set; }
    }
}
