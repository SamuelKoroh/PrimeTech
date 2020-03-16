using Microsoft.Extensions.Options;
using PrimeTech.Core.Services;
using PrimeTech.Infrastructure.AppSettings;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace PrimeTech.Data.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridSetting _sendGridSetting;
        public EmailService(IOptions<SendGridSetting> options)
        {
            _sendGridSetting = options.Value;
        }
        /// <summary>
        /// Send email message
        /// </summary>
        /// <param name="message">The email body</param>
        /// <param name="subject">Mail subject</param>
        /// <param name="to">Address of the receiver</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string message, string subject, string toAddress)
        {
           
            var client = new SendGridClient(_sendGridSetting.ApiKey);
            var from = new EmailAddress("noreply@example.com", "Prime Tech");
            var to = new EmailAddress(toAddress);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            await client.SendEmailAsync(msg);
        }
    }
}
