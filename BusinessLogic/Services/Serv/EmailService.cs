using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailResetPasswordAsync(ForgotPasswordModel forgotPasswordModel)
		{
			var smtpHost = _configuration["Smtp:Host"];
			var smtpPort = int.Parse(_configuration["Smtp:Port"]);
			var smtpUsername = _configuration["Smtp:Username"];
			var smtpPassword = _configuration["Smtp:Password"];
			var fromEmail = _configuration["Smtp:FromEmail"] ?? smtpUsername; // Fallback to Username
			var fromName = _configuration["Smtp:FromName"] ?? "Online Course Learning System";

			using var smtpClient = new SmtpClient(smtpHost, smtpPort)
			{
				Credentials = new NetworkCredential(smtpUsername, smtpPassword),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(fromEmail, fromName),
				Subject = "Đặt lại mật khẩu",
				Body = $@"
					<html>
					<body style='font-family: Arial, sans-serif;'>
						<h2>Yêu cầu đặt lại mật khẩu</h2>
						<p>Xin chào,</p>
						<p>Chúng tôi nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn.</p>
						<p>Vui lòng nhấp vào liên kết bên dưới để đặt lại mật khẩu:</p>
						<p><a href='{forgotPasswordModel.ResetLink}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Đặt lại mật khẩu</a></p>
						<p>Liên kết này sẽ hết hạn sau 2 phút.</p>
						<p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>
						<br/>
						<p>Trân trọng,</p>
						<p>OCMS Team</p>
					</body>
					</html>
				",
				IsBodyHtml = true
			};

			mailMessage.To.Add(forgotPasswordModel.Email);

			await smtpClient.SendMailAsync(mailMessage);
		}
		public async Task SendWelcomeEmailAsync(string toEmail, string username, string password)
		{
			var smtpHost = _configuration["Smtp:Host"];
			var smtpPort = int.Parse(_configuration["Smtp:Port"]);
			var smtpUsername = _configuration["Smtp:Username"];
			var smtpPassword = _configuration["Smtp:Password"];
			var fromEmail = _configuration["Smtp:FromEmail"] ?? smtpUsername;
			var fromName = _configuration["Smtp:FromName"] ?? "Online Course Learning System";

			using var smtpClient = new SmtpClient(smtpHost, smtpPort)
			{
				Credentials = new NetworkCredential(smtpUsername, smtpPassword),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(fromEmail, fromName),
				Subject = "Chào mừng bạn đến với OCMS - Thông tin tài khoản",
				Body = $@"
				<html>
				<body style='font-family: Arial, sans-serif;'>
					<h2>Chào mừng đến với Online Course Management System!</h2>
					<p>Xin chào <strong>{username}</strong>,</p>
					<p>Tài khoản của bạn đã được tạo thành công bởi quản trị viên.</p>
					<p>Dưới đây là thông tin đăng nhập của bạn:</p>
					<table style='border-collapse: collapse;'>
						<tr>
							<td style='padding: 8px; font-weight: bold;'>Tên đăng nhập:</td>
							<td style='padding: 8px;'>{username}</td>
						</tr>
						<tr>
							<td style='padding: 8px; font-weight: bold;'>Mật khẩu tạm thời:</td>
							<td style='padding: 8px; color: #e53935; font-size: 18px; font-weight: bold;'>{password}</td>
						</tr>
					</table>
					<br/>
					<p style='color: #f57c00;'><strong>Lưu ý:</strong> Vui lòng đổi mật khẩu ngay sau khi đăng nhập lần đầu.</p>
					<br/>
					<p>Trân trọng,</p>
					<p>OCMS Team</p>
				</body>
				</html>
				",
				IsBodyHtml = true
			};

			mailMessage.To.Add(toEmail);
			await smtpClient.SendMailAsync(mailMessage);
		}
	}
}
