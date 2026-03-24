namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AuditLog
    {
        public long AuditLogId { get; set; }

        public int? UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Action { get; set; }

        [StringLength(100)]
        public string Entity { get; set; }

        public string? OldValues { get; set; }

        public DateTime CreatedAt { get; set; }

        public string KeyValues { get; set; }

        public string? NewValues { get; set; }

		public string? IpAddress { get; set; }
		public string? UserAgent { get; set; }
		public int? DurationMs { get; set; }

		public virtual User User { get; set; }
    }
}
