using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using StudentsApplicationProj.Server.Models;
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
        public EmailSenderService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(SendGridModel model)
        {
            var apiKey = _config.GetValue<string>("sendGrid:apiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_config.GetValue<string>("sendGrid:from"), _config.GetValue<string>("sendGrid:name"));
            var to = new EmailAddress(model.To);
            var msg = MailHelper.CreateSingleEmail(from, to, model.Subject, model.PlainText, model.HtmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
