using System.Net.Mail;
using System.Net;
using E_Prescribing_API.Controllers;

namespace E_Prescribing_API.Data.Services
{
    public class EmailSender : IEmailSender
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                using var message = new MailMessage(Configuration["NetMail:sender"], email)
                {
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                using var smtp = new SmtpClient
                {
                    Host = Configuration["NetMail:smtpHost"],
                    EnableSsl = true,
                    Credentials = new NetworkCredential(
                        Configuration["NetMail:sender"],
                        Configuration["NetMail:senderpassword"]
                    ),
                    Port = 587,
                    UseDefaultCredentials = false
                };

                await smtp.SendMailAsync(message);
                _logger.LogInformation("Email successfully sent to {Email}", email);
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex,
                    "SMTP error sending email to {Email}. Possible invalid SMTP credentials or connection issue.",
                    email
                );
                throw new Exception("Failed to send email. Please verify SMTP settings.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error sending email to {Email}", email);
                throw;
            }
        }

    }
}
