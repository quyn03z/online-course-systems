namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
            Lessons = new HashSet<Lesson>();
            Payments = new HashSet<Payment>();
        }

        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsDelete { get; set; }

        public double Price { get; set; }
		public int Creator { get; set; }


		public int? CourseTypeId { get; set; }

        public virtual CourseType CourseType { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
