using System;

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
        public AuditLogUserDto User { get; set; }
    }

    public class AuditLogUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
