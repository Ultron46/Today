using DevOps.Common;
using DevOps.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using BuildProject = DevOps.UI.Models.BuildProject;
using EmailMaster = DevOps.UI.Models.EmailMaster;
using ServerConfig = DevOps.UI.Models.ServerConfig;
using User = DevOps.UI.Models.User;

namespace DevOps.UI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class ServersController : Controller
    {
        string baseUrl = Constants.baseurl;
        // GET: Servers
        public ActionResult Servers()
        {
            return PartialView("Servers");
        }
        [HttpGet]
        public async Task<ActionResult> Credentials()
        {
            string address1;
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (Session["Role"].ToString() == "Admin")
            {
                address1 = "api/Servers/GetServerConfigs?Organization=" + 0;
            }
            else
            {
                int id = Convert.ToInt32(Session["Organization"].ToString());
                address1 = "api/Servers/GetServerConfigs?Organization=" + id.ToString();
            }
            List<ServerConfig> servers = new List<ServerConfig>();
            HttpResponseMessage Res = await client.GetAsync(address1);
            Res = await client.GetAsync(address1);

            if (Res.IsSuccessStatusCode)
            {
                var ServersResponse = Res.Content.ReadAsStringAsync().Result;

                servers = JsonConvert.DeserializeObject<List<ServerConfig>>(ServersResponse);
            }
            ViewBag.Servers = servers;
            return PartialView("Credentials");
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return PartialView("Registration");
        }
        [HttpPost]
        public ActionResult Registration(Server server)

        {
            server.OrganisationId = Convert.ToInt32(Session["Organization"].ToString());
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringContent = new StringContent(JsonConvert.SerializeObject(server), Encoding.UTF8, "application/json");
            var addressUri = new Uri("api/Servers/InsertServer", UriKind.Relative);
            HttpResponseMessage Res = client.PostAsync(addressUri, stringContent).Result;
            if (Res.IsSuccessStatusCode)
            {
                return Json(new { success = true }); ;
            }
            return Json(new { error = true });
        }
        [HttpGet]
        public ActionResult InsertCredentials(int id)
        {
            ViewBag.serverID = id;
            return PartialView("InsertCredentials");
        }

        [HttpGet]
        public async Task<ActionResult> EditServer(int id)
        {
            ServerConfig serverConfig = new ServerConfig();
            ServerCredential serverCredential = new ServerCredential();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/Servers/GetServerConfig?id=" + id.ToString();
            HttpResponseMessage Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                serverConfig = JsonConvert.DeserializeObject<ServerConfig>(MainMEnuResponse);
            }
            return PartialView(serverConfig);
        }

        [HttpGet]
        public async Task<ActionResult> ShareServer(int id)
        {
            List<User> users = new List<User>();
            ServerConfig serverConfig = new ServerConfig();
            ServerCredential serverCredential = new ServerCredential();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address1 = "api/Servers/GetServerConfig?id=" + id.ToString();
            HttpResponseMessage Res = await client.GetAsync(address1);

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                serverConfig = JsonConvert.DeserializeObject<ServerConfig>(MainMEnuResponse);
                //var userTemp = serverConfig.Select(x => new SelectListItem { Text = x.Email, Value = x.UserId.ToString() }).ToList();
            }
            if (serverConfig != null)
            {
                var oid = serverConfig.OrganisationId;
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string address = "api/Users/GetAllUsersOfOrganization?id=" + oid.ToString();
                HttpResponseMessage Res1 = await client.GetAsync(address);

                if (Res1.IsSuccessStatusCode)
                {
                    var UserResponse = Res1.Content.ReadAsStringAsync().Result;

                    users = JsonConvert.DeserializeObject<List<User>>(UserResponse);
                    var userTemp = users.Select(x => new SelectListItem { Text = x.Email, Value = x.UserId.ToString() }).ToList();
                    if (Session["Role"].ToString() != "ReleaseManager")
                    {

                        userTemp = userTemp.Where(x => x.Value == Session["Organization"].ToString()).ToList();
                    }

                    ViewBag.To = userTemp;
                }

            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult ShareServerCredentials(Emails emails)

        {
            
            emails.From = "GDevOpsBuild@gmail.com";
            MailMessage mm = new MailMessage(emails.From, emails.To);
            mm.Subject = emails.Subject;

            mm.Body = emails.Body;
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            //smtp.Timeout = 10000;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("GDevOpsBuild@gmail.com", "DevopsBuildabcd");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            
        }

        public async Task<ActionResult> Builds()
        {
            List<DevOps.UI.Models.BuildProject> buildProjects = new List<DevOps.UI.Models.BuildProject>();
            string address1;
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (Session["Role"].ToString() == "Admin")
            {
                address1 = "api/Projects/GetProjectBuilds?id=" + 0;
            }
            else
            {
                int id = Convert.ToInt32(Session["Organization"].ToString());
                address1 = "api/Projects/GetProjectBuilds?id=" + id.ToString();
            }
            HttpResponseMessage Res = await client.GetAsync(address1);

            if (Res.IsSuccessStatusCode)
            {
                var BuildsResponse = Res.Content.ReadAsStringAsync().Result;

                buildProjects = JsonConvert.DeserializeObject<List<DevOps.UI.Models.BuildProject>>(BuildsResponse);
            }
            ViewBag.Builds = buildProjects.Select(x => new SelectListItem { Value = x.BuildId.ToString(), Text = x.Project.ProjectName +" " + x.Mejor_Version + "." + x.Minor_Version + "." + x.Build_Version});
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (Session["Role"].ToString() == "Admin")
            {
                address1 = "api/Servers/GetServerConfigs?Organization=" + 0;
            }
            else
            {
                int id = Convert.ToInt32(Session["Organization"].ToString());
                address1 = "api/Servers/GetServerConfigs?Organization=" + id.ToString();
            }
            List<ServerConfig> servers = new List<ServerConfig>();
            Res = await client.GetAsync(address1);

            if (Res.IsSuccessStatusCode)
            {
                var ServersResponse = Res.Content.ReadAsStringAsync().Result;

                servers = JsonConvert.DeserializeObject<List<ServerConfig>>(ServersResponse);
            }
            ViewBag.Servers = servers;
            return PartialView();
        }

        public async Task<ActionResult> StartServerBuild(int BuildId, int ServerId)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string addressUrl = "api/EmailMaster/GetEmails?id=" + Session["Organization"].ToString();
            List<EmailMaster> emails = new List<EmailMaster>();
            HttpResponseMessage Res = await client.GetAsync(addressUrl);
            if (Res.IsSuccessStatusCode)
            {
                var Emails = Res.Content.ReadAsStringAsync().Result;
                emails = JsonConvert.DeserializeObject<List<EmailMaster>>(Emails);
            }
            List<string> emailIds = emails.Select(x => x.EmailId).ToList();
            if (!emailIds.Contains(Session["Username"].ToString()))
            {
                emailIds.Add(Session["Username"].ToString());
            }
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            addressUrl = "api/Projects/GetProjectBuild?id=" + BuildId;
            BuildProject build = new BuildProject();
            Res = await client.GetAsync(addressUrl);
            if (Res.IsSuccessStatusCode)
            {
                var Project = Res.Content.ReadAsStringAsync().Result;
                build = JsonConvert.DeserializeObject<BuildProject>(Project);
            }
            string subject = build.Project.ProjectName + " " + build.Mejor_Version + "." + build.Minor_Version + "." + build.Build_Version;
            Helpers.SendEmail(emailIds, subject, "New Server Build Started for the projct build" + subject);
            string address = ConfigurationManager.AppSettings["ServerBuildAPI"] + build.DownloadURL;
            Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                client.DefaultRequestHeaders.Clear();
                int userId = Convert.ToInt32(Session["user"].ToString());
                address = "api/Servers/InsertServerBuild?BuildId=" + BuildId + "&ServerID=" + ServerId + "&UserId=" + userId;
                Res = await client.GetAsync(address);
                if (Res.IsSuccessStatusCode)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }
    }

}
 
