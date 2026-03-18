using BusinessLogic.Claims;
using BusinessLogic.Models.Momo;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
	public static class ApplicationDependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IHostEnvironment env)
		{
			services.AddServices(env);

			return services;
		}
		private static void AddServices(this IServiceCollection services, IHostEnvironment env)
		{
			// user
			services.AddScoped<IUserService, UserService>();

			// role
			services.AddScoped<IRoleService, RoleService>();

			//email
			services.AddScoped<IEmailService, EmailService>();

			//claim service
			services.AddScoped<IClaimService, ClaimService>();

			// course
			services.AddScoped<ICourseService, CourseService>();

			// lesson
			services.AddScoped<ILessonService, LessonService>();

			// sublesson
			services.AddScoped<ISubLessonService, SubLessonService>();

			// quizz
			services.AddScoped<IQuizzService, QuizzService>();
			
			// questions
			services.AddScoped<IQuestionsService, QuestionsService>();

			// momo
			services.AddScoped<IMomoService, MomoService>();

			// enrollment
			services.AddScoped<IEnrollmentService, EnrollmentService>();

			// payment
			services.AddScoped<IPaymentService, PaymentService>();

			// auditlogs
			services.AddScoped<IAuditLogsService, AuditLogsService>();

			// courseType
			services.AddScoped<ICourseTypeService, CourseTypeService>();

			// documents
			services.AddScoped<IDocumentsService, DocumentsService>();

			// admin
			services.AddScoped<IAdminService,AdminService>();

			// menteeScore
			services.AddScoped<IMenteeScoreService,MenteeScoreService>();

			// permission
			services.AddScoped<IPermissionService, PermissionService>();
		}

	}
}
