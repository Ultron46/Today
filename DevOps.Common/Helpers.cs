using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async static Task<int> GetResponse(string uri)
        {
            string baseUrl = Constants.baseurl;
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync(uri);
            int total = 0;
            if (Res.IsSuccessStatusCode)
            {
                var BuildsResponse = Res.Content.ReadAsStringAsync().Result;

                total = JsonConvert.DeserializeObject<int>(BuildsResponse);
            }
            return total;
        }

        public async static Task<HttpResponseMessage> Get(string uri)
        {
            string baseUrl = Constants.baseurl;
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync(uri);
            return Res;
        }
    }
}
