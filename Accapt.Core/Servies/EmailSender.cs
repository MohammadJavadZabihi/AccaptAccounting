using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailViewModel emailViewModel)
        {
            var emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "ConfirmEmail.html");
            var emailBody = await File.ReadAllTextAsync(emailTemplatePath);
            emailBody = emailBody.Replace("{{confirmationLink}}", emailViewModel.Body);

            var message = new MailMessage
            {
                From = new MailAddress("Accapt@accaptacounting.ir", "حسابداری آکپت"),
                Body = emailBody,
                Subject = emailViewModel.Subject,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(emailViewModel.To));

            using (var smtpClient = new SmtpClient("webmail.accaptacounting.ir", 587))
            {
                smtpClient.Credentials = new NetworkCredential("Accapt@accaptacounting.ir", "mahan.z.road0908");
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                await smtpClient.SendMailAsync(message);
            }
        }
    }
}
