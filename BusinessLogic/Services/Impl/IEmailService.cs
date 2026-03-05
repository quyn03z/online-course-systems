using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IEmailService
	{
		Task SendEmailResetPasswordAsync(ForgotPasswordModel forgotPasswordModel);
		Task SendWelcomeEmailAsync(string toEmail, string username, string password);
	}
}
