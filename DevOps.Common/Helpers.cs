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
using System.Web;

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
            string hash = Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(password))
                ).Replace('"', ' ');
            hash = hash.Replace('+', ' ');
            return hash;
        }

        public async static Task SendEmail(List<string> emailids,string subject, string body)
        {
            MailAddress From = new MailAddress("GDevOpsBuild@gmail.com");
            string Password = "DevopsBuildabcd";
            using (var message = new MailMessage())
            {
                message.IsBodyHtml = true;
                message.From = From;
                foreach (string id in emailids)
                {
                    message.To.Add(id);
                }
                message.Subject = subject;
                message.Body = "<html><body>" +

                    "<center><img src=\'https://alln-extcloud-storage.cisco.com/ciscoblogs/5d37d7284e6e8.png \' height=\'200px \' width=\'auto \' /></center>" +
                    "<h3>Hello!!!!</h3>" + 
                    body +
                    "</body></html>";
                using (var smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Timeout = 10000;
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    NetworkCredential nc = new NetworkCredential(From.Address, Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    await smtp.SendMailAsync(message);
                }
            }
        }

        public async static Task<int> GetResponse(string uri, string token)
        {
            string baseUrl = Constants.baseurl;
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage Res = await client.GetAsync(uri);
            int total = 0;
            if (Res.IsSuccessStatusCode)
            {
                var BuildsResponse = Res.Content.ReadAsStringAsync().Result;

                total = JsonConvert.DeserializeObject<int>(BuildsResponse);
            }
            return total;
        }

        public async static Task<HttpResponseMessage> Get(string uri, string token)
        {
            string baseUrl = Constants.baseurl;
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage Res = await client.GetAsync(uri);
            client.Dispose();
            return Res;
        }

        public static HttpResponseMessage Post(Uri uri, StringContent content, string token)
        {
            string baseUrl = Constants.baseurl;
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage Res = client.PostAsync(uri, content).Result;
            return Res;
        }
    }
}
