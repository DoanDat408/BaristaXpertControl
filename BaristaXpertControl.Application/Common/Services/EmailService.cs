using System.Net;
using System.Net.Mail;
using BaristaXpertControl.Application.Common.Persistences;
using Microsoft.Extensions.Configuration;

namespace BaristaXpertControl.Application.Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly bool _enableSsl;

        public EmailService(IConfiguration configuration)
        {
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _smtpUsername = configuration["EmailSettings:SmtpUsername"];
            _smtpPassword = configuration["EmailSettings:SmtpPassword"];
            _enableSsl = bool.Parse(configuration["EmailSettings:EnableSsl"]);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    smtpClient.EnableSsl = _enableSsl;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUsername, "BaristaXpertControl System"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(to); // Gửi tới email người dùng

                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine($"Email sent successfully to {to}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw;
            }
        }
    }
}
