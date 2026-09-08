using Domain.Helpers.EmailInterface;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers.EmailServicefolder
{
    public class Emailserviceclass:IEmailservice
    {
        private readonly IConfiguration _configuration;

        public Emailserviceclass(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendRegistrationEmailAsync(
            string email,
            string firstName,
            string role)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();

            message.From.Add(
                new MailboxAddress(
                    emailSettings["SenderName"],
                    emailSettings["SenderEmail"]
                )
            );
            message.To.Add(
               new MailboxAddress(
                   firstName,
                   email
               )
           );
            message.Subject = "Welcome to Job Portal";

            var body = $@"
                <html>
                <body>
                    <h2>Welcome {firstName}!</h2>

                    <p>Your registration has been successfully completed.</p>

                    <p>
                        <strong>Role:</strong> {role}
                    </p>

                    <p>
                        Your email has been verified successfully.
                    </p>

                    <p>
                        You can now create your password and complete
                        your account setup.
                    </p>

                    <br/>

                    <p>Thank you,<br/>
                    Job Portal Team</p>
                </body>
                </html>";

            message.Body = new BodyBuilder
            {
                HtmlBody = body
            }.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                emailSettings["SmtpServer"],
                int.Parse(emailSettings["Port"]!),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                emailSettings["SenderEmail"],
                emailSettings["Password"]
            );
            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
    }
}