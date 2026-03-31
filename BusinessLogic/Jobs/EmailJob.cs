using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Jobs
{
    /// <summary>
    /// Hangfire background job xử lý gửi email không đồng bộ.
    /// </summary>
    public class EmailJob
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailJob> _logger;

        public EmailJob(IEmailService emailService, ILogger<EmailJob> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        /// Gửi email chào mừng user mới (chạy nền qua Hangfire).
        /// </summary>
        public async Task SendWelcomeEmailAsync(string toEmail, string username, string password)
        {
            try
            {
                _logger.LogInformation("Bắt đầu gửi welcome email đến {Email}", toEmail);
                await _emailService.SendWelcomeEmailAsync(toEmail, username, password);
                _logger.LogInformation("Gửi welcome email thành công đến {Email}", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi email đến {Email}", toEmail);
                throw; // Hangfire sẽ retry tự động
            }
        }

        public async Task SendEmailResetPasswordAsync(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                _logger.LogInformation("Bắt đầu gửi email reset password đến {Email}", forgotPasswordModel.Email);
                await _emailService.SendEmailResetPasswordAsync(forgotPasswordModel);
                _logger.LogInformation("Gửi email reset password thành công đến {Email}", forgotPasswordModel.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi email reset password đến {Email}", forgotPasswordModel.Email);
                throw;
            }
        }
    }
}
