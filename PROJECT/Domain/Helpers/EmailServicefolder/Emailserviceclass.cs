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


        public async Task SendCompanyAcceptedEmailAsync(string email, string companyname)
        {
            var emailsetting = _configuration.GetSection("EmailSettings");
            var message = new MimeMessage();
            message.From.Add(
                new MailboxAddress(
                    emailsetting["SenderName"],
                    emailsetting["SenderEmail"]));
            message.To.Add(
                new MailboxAddress(
                    companyname,
                    email)
                );
            message.Subject = "Company Registration Accepted";

            var body = $@"
        <html>
        <body>
            <h2>Congratulations {companyname}!</h2>

            <p>
                Your company registration has been reviewed
                and accepted by the Job Portal Admin.
            </p>

            <p>
                Your company account is now approved and
                you can continue using the Job Portal.
            </p>

            <br/>

            <p>
                Thank you,<br/>
                Job Portal Team
            </p>
        </body>
        </html>";
            message.Body=new BodyBuilder
            {
                HtmlBody=body
            }.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                emailsetting["SmtpServer"],
                int.Parse(emailsetting["Port"]!),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                emailsetting["SenderEmail"],
                emailsetting["Password"]
            );

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }

        public async Task sendCompanyRejectedEmailAsync(string email, string companyname)

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
                    companyname,
                    email
                )
            );

            message.Subject = "Company Registration Rejected";

            var body = $@"
        <html>
        <body>
            <h2>Hello {companyname},</h2>

            <p>
                Your company registration has been reviewed
                by the Job Portal Admin.
            </p>

            <p>
                Unfortunately, your company registration
                has been rejected at this time.
            </p>

            <p>
                Please contact the Job Portal Administration
                for further information.
            </p>

            <br/>

            <p>
                Thank you,<br/>
                Job Portal Team
            </p>
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