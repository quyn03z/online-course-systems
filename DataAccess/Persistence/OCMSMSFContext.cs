using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.Claims;

namespace DataAccess.Repositories
{
	public partial class OCMSMSFContext : DbContext
	{
		private readonly IClaimService? _claimService;

		public OCMSMSFContext(DbContextOptions<OCMSMSFContext> options, IClaimService? claimService = null)
	   : base(options)
		{
			_claimService = claimService;
		}
		

		public virtual DbSet<Answer> Answers { get; set; }
		public virtual DbSet<AuditLog> AuditLogs { get; set; }
		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<CourseType> CourseType { get; set; }
		public virtual DbSet<Enrollment> Enrollments { get; set; }
		public virtual DbSet<Lesson> Lessons { get; set; }
		public virtual DbSet<Documents> Documents { get; set; }
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

			modelBuilder.Entity<Lesson>()
				.HasMany(e => e.Documents)
				.WithOne(e => e.Lesson)
				.HasForeignKey(e => e.LessonId)
				.OnDelete(DeleteBehavior.Restrict);

			// Payment
			modelBuilder.Entity<Payment>()
				.Property(e => e.Amount)
				.HasColumnType("decimal(10,2)");

			// Question — khai báo rõ FK để EF không sinh QuestionTypeTypeId
			modelBuilder.Entity<Question>()
				.HasOne(q => q.QuestionType)
				.WithMany()
				.HasForeignKey(q => q.TypeId);

			// Quizz
			modelBuilder.Entity<Quizz>()
				.HasMany(e => e.MenteeScores)
				.WithOne(e => e.Quizz)
				.HasForeignKey(e => e.QuizId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Quizz>()
				.HasMany(e => e.Questions)
				.WithOne(e => e.Quizz)
				.HasForeignKey(e => e.QuizzId);

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

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var auditEntries = OnBeforeSaveChanges();
			var result = await base.SaveChangesAsync(cancellationToken);
			await OnAfterSaveChangesAsync(auditEntries);
			return result;
		}

		public override int SaveChanges()
		{
			var auditEntries = OnBeforeSaveChanges();
			var result = base.SaveChanges();
			OnAfterSaveChanges(auditEntries);
			return result;
		}

		private List<AuditEntry> OnBeforeSaveChanges()
		{
			ChangeTracker.DetectChanges();
			var auditEntries = new List<AuditEntry>();
			foreach (var entry in ChangeTracker.Entries())
			{
				if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
					continue;

				var auditEntry = new AuditEntry(entry);
				auditEntry.TableName = entry.Entity.GetType().Name;
				auditEntry.UserId = _claimService?.GetUserId();
				auditEntries.Add(auditEntry);

				foreach (var property in entry.Properties)
				{
					string propertyName = property.Metadata.Name;
					if (property.IsTemporary)
					{
						auditEntry.TemporaryProperties.Add(property);
						continue;
					}

					if (property.Metadata.IsPrimaryKey())
					{
						auditEntry.KeyValues[propertyName] = property.CurrentValue;
						continue;
					}

					switch (entry.State)
					{
						case EntityState.Added:
							auditEntry.NewValues[propertyName] = property.CurrentValue;
							break;

						case EntityState.Deleted:
							auditEntry.OldValues[propertyName] = property.OriginalValue;
							break;

						case EntityState.Modified:
							if (property.IsModified)
							{
								auditEntry.OldValues[propertyName] = property.OriginalValue;
								auditEntry.NewValues[propertyName] = property.CurrentValue;
							}
							break;
					}
				}
			}

			foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
			{
				AuditLogs.Add(auditEntry.ToAuditLog());
			}

			return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
		}

		private Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
		{
			if (auditEntries == null || auditEntries.Count == 0)
				return Task.CompletedTask;

			foreach (var auditEntry in auditEntries)
			{
				foreach (var prop in auditEntry.TemporaryProperties)
				{
					if (prop.Metadata.IsPrimaryKey())
					{
						auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
					}
					else
					{
						auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
					}
				}

				AuditLogs.Add(auditEntry.ToAuditLog());
			}

			return base.SaveChangesAsync();
		}

		private void OnAfterSaveChanges(List<AuditEntry> auditEntries)
		{
			if (auditEntries == null || auditEntries.Count == 0)
				return;

			foreach (var auditEntry in auditEntries)
			{
				foreach (var prop in auditEntry.TemporaryProperties)
				{
					if (prop.Metadata.IsPrimaryKey())
					{
						auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
					}
					else
					{
						auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
					}
				}

				AuditLogs.Add(auditEntry.ToAuditLog());
			}

			base.SaveChanges();
		}
	}
}
