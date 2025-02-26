﻿using ikifikir.CORE.EmailConfig;
using ikifikir.ENGINES.Interface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Engines
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task<string> SendEmailAsync(Message message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse(_emailConfig.From));
                emailMessage.To.Add(MailboxAddress.Parse(message.To));
                emailMessage.Subject = message.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<div>{0}</div>", message.Content) };

                using (var client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, false);
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                }
                return "Başarılı!";
            }
            catch (Exception ex)
            {
                return ex.ToString();

            }
        }

        public async Task<string> SendEmailAsync(AppoinmentContact contact)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse(_emailConfig.From));
                emailMessage.To.Add(MailboxAddress.Parse(contact.To));
                emailMessage.Subject = contact.Subject;
                emailMessage.Date = DateTime.Now;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<div>{0}</div>", contact.Content) };

                using (var client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, false);
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                }
                return "Başarılı!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
