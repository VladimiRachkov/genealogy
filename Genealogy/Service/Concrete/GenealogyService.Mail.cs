using System.Threading.Tasks;
using Genealogy.Service.Astract;
using MimeKit;
using MailKit.Net.Smtp;
using Genealogy.Models;
using Genealogy.Data;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public async Task SendEmailAsync(string subject, string message, string emailFrom, string name)
        {
            var adminEmail = GetSettingValue(Settings.AdminEmail);
            var serviceEmail = GetSettingValue(Settings.ServiceEmail);
            var serviceEmailPassword = GetSettingValue(Settings.ServiceEmailPassword);
            var emailTitle = GetSettingValue(Settings.EmailTitle);

            var smtpServer = GetSettingValue(Settings.SmtpServer);
            var smtpServerPort = Convert.ToInt16(GetSettingValue(Settings.SmtpServerPort));
            var smtpServerSsl = Convert.ToBoolean(GetSettingValue(Settings.SmtpServerSsl));

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(emailTitle, serviceEmail));
            emailMessage.To.Add(new MailboxAddress(subject, adminEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpServerPort, false);
                await client.AuthenticateAsync(serviceEmail, serviceEmailPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async void SendMessage(BusinessObjectInDto boDto)
        {
            if (boDto.MetatypeId == MetatypeData.Message.Id)
            {
                var data = JsonConvert.DeserializeObject<MessageData>(boDto.Data);

                var title = $"{data.Username} - {boDto.Title}";
                var message = $"<h3>{boDto.Title}</h3><h4>{data.Username} - {data.Email}</h4><p>{data.Message}</p>";

                await SendEmailAsync(title, message, data.Email, data.Username);
            }
        }

        public async Task<bool> SendEmailToUser(string subject, string email, string message)
        {
            var serviceEmail = GetSettingValue(Settings.ServiceEmail);
            var serviceEmailPassword = GetSettingValue(Settings.ServiceEmailPassword);
            var emailTitle = GetSettingValue(Settings.EmailTitle);

            var smtpServer = GetSettingValue(Settings.SmtpServer);
            var smtpServerPort = Convert.ToInt16(GetSettingValue(Settings.SmtpServerPort));
            var smtpServerSsl = Convert.ToBoolean(GetSettingValue(Settings.SmtpServerSsl));

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(emailTitle, serviceEmail));
            emailMessage.To.Add(new MailboxAddress(subject, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpServerPort, true);
                await client.AuthenticateAsync(serviceEmail, serviceEmailPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);

                return true;
            }
        }
    }
}