using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models
{
    class EmailOTP
    {
        public int EmailOTPMethod(string pEmail, string username)
        {
            string To_Addr = pEmail;
            string Name = username;
            GetRandomNumber RG = new GetRandomNumber();
            int OTP = RG.RandomNumber(100000, 999999);
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential MyCredentials = new NetworkCredential("sravanthireddy1889@gmail.com", "P@ssw0rd@1234567");
            smtpClient.EnableSsl = true;
            mailMessage.To.Add(new MailAddress($"{To_Addr}"));
            mailMessage.From = new MailAddress("sravanthireddy1889@gmail.com");
            mailMessage.Subject = "Student Hub OTP Email";
            mailMessage.Body = $"Hello {Name},\n\n" +
                $"Your 6 digit OTP from Student Hub : {OTP}\n\n" +
                $"Thanks,\nAdmin\nStudent Hub";

            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = MyCredentials;

            smtpClient.Send(mailMessage);
            return OTP;
        }
    }
}
