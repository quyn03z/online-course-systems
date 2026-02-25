using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DataAccess.Repositories
{
	public partial class OCMSMSFContext : DbContext
	{

		public OCMSMSFContext(DbContextOptions<OCMSMSFContext> options)
	   : base(options)
		{
		}
		

		public virtual DbSet<Answer> Answers { get; set; }
		public virtual DbSet<AuditLog> AuditLogs { get; set; }
		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<CourseType> CourseType { get; set; }
		public virtual DbSet<Enrollment> Enrollments { get; set; }
		public virtual DbSet<Lesson> Lessons { get; set; }
		public virtual DbSet<MenteeScores> MenteeScores { get; set; }
		public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<QuestionType> QuestionType { get; set; }
		public virtual DbSet<Question> Questions { get; set; }
		public virtual DbSet<Quizz> Quizzs { get; set; }
		public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
		public virtual DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<SubLesson> SubLessons { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Enrollment - Composite primary key
			modelBuilder.Entity<Enrollment>()
				.HasKey(e => new { e.UserId, e.CourseId });

			modelBuilder.Entity<MenteeScores>()
				.HasKey(e => new {e.UserId, e.QuizId});

			// Answer
			modelBuilder.Entity<Answer>()
				.Property(e => e.AnswerText)
				.IsUnicode(false);

			// Course
			modelBuilder.Entity<Course>()
				.Property(e => e.CourseName)
				.IsFixedLength();

			modelBuilder.Entity<Course>()
				.HasMany(e => e.Enrollments)
				.WithOne(e => e.Course)
				.HasForeignKey(e => e.CourseId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Course>()
				.HasMany(e => e.Lessons)
				.WithOne(e => e.Course)
				.HasForeignKey(e => e.CourseId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Course>()
				.HasMany(e => e.Payments)
				.WithOne(e => e.Course)
				.HasForeignKey(e => e.CourseId)
				.OnDelete(DeleteBehavior.SetNull);

			// Course_Type
			modelBuilder.Entity<CourseType>()
				.HasMany(e => e.Courses)
				.WithOne(e => e.CourseType)
				.HasForeignKey(e => e.CourseTypeId);

			// Lesson
			modelBuilder.Entity<Lesson>()
				.HasOne(e => e.Quizz)
				.WithOne(e => e.Lesson)
				.HasForeignKey<Quizz>(e => e.LessonId);

			modelBuilder.Entity<Lesson>()
				.HasMany(e => e.SubLessons)
				.WithOne(e => e.Lesson)
				.HasForeignKey(e => e.LessonId)
				.OnDelete(DeleteBehavior.Restrict);

			// Payment
			modelBuilder.Entity<Payment>()
				.Property(e => e.Amount)
				.HasColumnType("decimal(10,2)");

			// Quizz
			modelBuilder.Entity<Quizz>()
				.HasMany(e => e.MenteeScores)
				.WithOne(e => e.Quizz)
				.HasForeignKey(e => e.QuizId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Quizz>()
				.HasMany(e => e.Questions)
				.WithOne(e => e.Quizz)
				.HasForeignKey(e => e.QuizId);

			// Role
			modelBuilder.Entity<Role>()
				.HasMany(e => e.Users)
				.WithOne(e => e.Role)
				.HasForeignKey(e => e.RoleId)
				.OnDelete(DeleteBehavior.Restrict);

			// User relationships
			modelBuilder.Entity<User>()
				.HasMany(e => e.Enrollments)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Mentee_scores)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
				.HasMany(e => e.RefreshTokens)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
				.HasMany(e => e.ResetPasswordTokens)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
