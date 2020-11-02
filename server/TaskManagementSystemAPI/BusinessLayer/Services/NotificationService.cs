using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace BusinessLayer.Services
{
    public class NotificationService: INotificationService
    {
        private string SendMail = "apriorittm@gmail.com";
        private string SendPassword = "Qwerty2020";
        public async void SendEmailAsync(string email, string subject, string message)
        {

            var emailMessage = new MimeMessage();
            // Добавление информации об отправителе
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", SendMail));
            // добавление инофрмации о получателе
            emailMessage.To.Add(new MailboxAddress("", email));
            // тема письма
            emailMessage.Subject = subject;
            // добавления тела месседжа
            emailMessage.Body = new TextPart("Plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                // подключение к сервису почты
                await client.ConnectAsync("smtp.gmail.com", 25, false);
                // аутентификация в почте
                await client.AuthenticateAsync(SendMail, SendPassword);
                // отправка месседжа
                await client.SendAsync(emailMessage);
                // отключения от сервиса почты
                await client.DisconnectAsync(true);
            }
        }
    }
}
