using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Common
{
    public static class Helpers
    {
        public static string GenerateToken(int id,string password)
        {
            string token = id + ":" + password.GetHashCode() + DateTime.Now.GetHashCode().ToString();
            return token;
        }

        public static string Hash(string password)
        {
            return password.GetHashCode().ToString();
        }

        public static void SendEmail(List<string> emailids,string subject, string body)
        {
            string From = "GDevOpsBuild@gmail.com";
            string Password = "DevopsBuildabcd";
            foreach (string id in emailids)
            {
                MailMessage mm = new MailMessage(From, id);
                mm.Subject = subject;

                mm.Body = body;
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                //smtp.Timeout = 10000;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential(From, Password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(mm);
            }
        }
    }
}
