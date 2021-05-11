using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using StudentsApplicationProj.Server.Models;
using System;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Services
{
    public interface IEmailSenderService
    {
        Task SendEmail(SendGridModel model);
    }

    public class EmailSenderService: IEmailSenderService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailSenderService> _logger;
        public EmailSenderService(IConfiguration config, ILogger<EmailSenderService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendEmail(SendGridModel model)
        {
            try
            {
                var apiKey = _config.GetValue<string>("sendGrid:apiKey");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(_config.GetValue<string>("sendGrid:from"), _config.GetValue<string>("sendGrid:name"));
                var to = new EmailAddress(model.To);
                var msg = MailHelper.CreateSingleEmail(from, to, model.Subject, model.PlainText, model.HtmlContent);
                await client.SendEmailAsync(msg);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
        }
    }
}
