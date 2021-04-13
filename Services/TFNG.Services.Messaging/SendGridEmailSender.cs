﻿namespace ТАК.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SendGrid;
    using SendGrid.Helpers.Mail;
    using TFNG.Services.Messaging;

    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridClient client;

        public SendGridEmailSender(string apiKey)
        {
            this.client = new SendGridClient(apiKey);
        }

        public async Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(htmlContent) || string.IsNullOrWhiteSpace(fromName) || string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("Моля, въведете всички полета на контактната форма!");
            }

            var fromAddress = new EmailAddress("sharwinchester@gmail.com", fromName);
            var toAddress = new EmailAddress(to);
            string content = $"<strong>Не отговаряйте на този имейл! Изпратете отговора си до получателя: {from}!</strong><br /><strong>От:</strong><br /><strong>Име: {fromName}</strong><br /><strong>Относно: {subject}</strong><br /><hr>{htmlContent}";
            var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, null, content);
            if (attachments?.Any() == true)
            {
                foreach (var attachment in attachments)
                {
                    message.AddAttachment(attachment.FileName, Convert.ToBase64String(attachment.Content), attachment.MimeType);
                }
            }

            try
            {
                var response = await this.client.SendEmailAsync(message);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Body.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
