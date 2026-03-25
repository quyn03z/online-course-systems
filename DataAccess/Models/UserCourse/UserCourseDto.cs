using System;

namespace DataAccess.Models.UserCourse
{
    public class UserCourseDto
    {
        public int Id { get; set; } // Fallback for Id column
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? Date { get; set; }
    }
}
