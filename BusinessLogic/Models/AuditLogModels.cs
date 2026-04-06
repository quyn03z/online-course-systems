using System;
using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class AuditLogResponseModel
    {
        public long AuditLogId { get; set; }
        public int? UserId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string KeyValues { get; set; }
        public DateTime CreatedAt { get; set; }
		public string IpAddress { get; set; }
		public string UserAgent { get; set; }
		public int? DurationMs { get; set; }
		public AuditLogUserDto User { get; set; }
        public List<AuditLogChangeDto> Changes { get; set; } = new List<AuditLogChangeDto>();
    }

    public class AuditLogUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class AuditLogChangeDto
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
