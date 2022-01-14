using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Onnorokom.ShoppingCart.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Common.Services
{
    public class EmailService : IEmailService
    {

        private ConfirmationEmailSettings _confirmationEmailSettings;
        public EmailService(IOptions<ConfirmationEmailSettings> confirmationEmailSettings)
        {
            _confirmationEmailSettings = confirmationEmailSettings.Value;
        }
        public void SendEmail(string receiver, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_confirmationEmailSettings.from, _confirmationEmailSettings.from));
            message.To.Add(new MailboxAddress(receiver, receiver));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body,
            };

            using var client = new SmtpClient();
            client.Timeout = 60000;
            client.Connect(_confirmationEmailSettings.host, _confirmationEmailSettings.port, _confirmationEmailSettings.useSSL);


            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(_confirmationEmailSettings.username, _confirmationEmailSettings.password);

            client.Send(message);
            client.Disconnect(true);
        }

    }
}
