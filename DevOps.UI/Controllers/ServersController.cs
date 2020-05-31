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

namespace DevOps.UI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [RoleAuth("Admin", "ReleaseManager")]
    public class ServersController : Controller
    {
        // GET: Servers
        public ActionResult Servers()
        {
            return PartialView("Servers");
        }
        [HttpGet]
        public async Task<ActionResult> Credentials()
        {
            string address1;
            string token = Session["UserToken"].ToString();
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
            HttpResponseMessage Res = await Helpers.Get(address1, token);
            if (Res.IsSuccessStatusCode)
            {
                var ServersResponse = Res.Content.ReadAsStringAsync().Result;
                servers = JsonConvert.DeserializeObject<List<ServerConfig>>(ServersResponse);
            }
            ViewBag.Servers = servers;
            return PartialView("Credentials");
        }
        [HttpGet]
        public async Task<ActionResult> Registration()
        {
            string address1;
            string token = Session["UserToken"].ToString();
            HttpResponseMessage Res;
            if (Session["Role"].ToString() == "Admin")
            {
                address1 = "api/Organizations/GetAllOrganization";
                List<Organization> organizations = new List<Organization>();
                Res = await Helpers.Get(address1, token);
                if (Res.IsSuccessStatusCode)
                {
                    var Projects = Res.Content.ReadAsStringAsync().Result;
                    organizations = JsonConvert.DeserializeObject<List<Organization>>(Projects);
                }
                ViewBag.Orgs = organizations;
            }
            return PartialView("Registration");
        }
        [HttpPost]
        public ActionResult Registration(Server server)
        {
            string token = Session["UserToken"].ToString();
            server.OrganisationId = Convert.ToInt32(Session["Organization"].ToString());
            var stringContent = new StringContent(JsonConvert.SerializeObject(server), Encoding.UTF8, "application/json");
            var addressUri = new Uri("api/Servers/InsertServer", UriKind.Relative);
            HttpResponseMessage Res = Helpers.Post(addressUri, stringContent, token);
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
            string token = Session["UserToken"].ToString();
            string address = "api/Servers/GetServerConfig?id=" + id.ToString();
            HttpResponseMessage Res = await Helpers.Get(address, token);
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
            string token = Session["UserToken"].ToString();
            List<User> users = new List<User>();
            ServerConfig serverConfig = new ServerConfig();
            ServerCredential serverCredential = new ServerCredential();
            string address1 = "api/Servers/GetServerConfig?id=" + id.ToString();
            HttpResponseMessage Res = await Helpers.Get(address1, token);
            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                serverConfig = JsonConvert.DeserializeObject<ServerConfig>(MainMEnuResponse);
            }
            if (serverConfig != null)
            {
                var oid = serverConfig.OrganisationId;
                string address = "api/Users/GetAllUsersOfOrganization?id=" + oid.ToString();
                HttpResponseMessage Res1 = await Helpers.Get(address, token);
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

        [HttpGet]
        public async Task<ActionResult> EditCredential(int id)
        {
            ServerCredential credential = new ServerCredential();
            string token = Session["UserToken"].ToString();
            string address = "api/Servers/GetCredential?id=" + id.ToString();
            HttpResponseMessage Res = await Helpers.Get(address, token);
            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                credential = JsonConvert.DeserializeObject<ServerCredential>(MainMEnuResponse);
            }
            return PartialView(credential);
        }

        [HttpPost]
        public ActionResult ShareServerCredentials(Emails emails)
        {
            emails.From = "GDevOpsBuild@gmail.com";
            MailMessage mm = new MailMessage(emails.From, emails.To);
            mm.Subject = emails.Subject;
            mm.Body = emails.Body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("GDevOpsBuild@gmail.com", "DevopsBuildabcd");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Builds()
        {
            string address1;
            string token = Session["UserToken"].ToString();
            HttpResponseMessage Res;
            if (Session["Role"].ToString() == "Admin")
            {
                address1 = "api/Organizations/GetAllOrganization";
                List<Organization> organizations = new List<Organization>();
                Res = await Helpers.Get(address1, token);
                if (Res.IsSuccessStatusCode)
                {
                    var Projects = Res.Content.ReadAsStringAsync().Result;
                    organizations = JsonConvert.DeserializeObject<List<Organization>>(Projects);
                }
                ViewBag.Orgs = organizations;
            }
            else
            {
                address1 = "api/Projects/GetOrganizationProject?id=" + Session["Organization"].ToString();
                List<Project> projects = new List<Project>();
                Res = await Helpers.Get(address1, token);
                if (Res.IsSuccessStatusCode)
                {
                    var Projects = Res.Content.ReadAsStringAsync().Result;
                    projects = JsonConvert.DeserializeObject<List<Project>>(Projects);
                }
                ViewBag.Projects = projects;
                int id = Convert.ToInt32(Session["Organization"].ToString());
                address1 = "api/Servers/GetServerConfigs?Organization=" + id.ToString();
                List<ServerConfig> servers = new List<ServerConfig>();
                Res = await Helpers.Get(address1, token);
                if (Res.IsSuccessStatusCode)
                {
                    var ServersResponse = Res.Content.ReadAsStringAsync().Result;
                    servers = JsonConvert.DeserializeObject<List<ServerConfig>>(ServersResponse);
                }
                ViewBag.Servers = servers;
            }
            return PartialView();
        }

        [HttpGet]
        public async Task<ActionResult> StartServerBuild(int BuildId, int ServerId)
        {
            string token = Session["UserToken"].ToString();
            string addressUrl = "api/EmailMaster/GetEmails?id=" + Session["Organization"].ToString();
            List<EmailMaster> emails = new List<EmailMaster>();
            HttpResponseMessage Res = await Helpers.Get(addressUrl, token);
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
            addressUrl = "api/Projects/GetProjectBuild?id=" + BuildId;
            BuildProject build = new BuildProject();
            Res = await Helpers.Get(addressUrl, token);
            if (Res.IsSuccessStatusCode)
            {
                var Project = Res.Content.ReadAsStringAsync().Result;
                build = JsonConvert.DeserializeObject<BuildProject>(Project);
            }
            string subject = build.Project.ProjectName + " " + build.Mejor_Version + "." + build.Minor_Version + "." + build.Build_Version;
            await Helpers.SendEmail(emailIds, subject, "New Server Build Started for the projct build" + subject);
            string address = ConfigurationManager.AppSettings["ServerBuildAPI"] + build.DownloadURL;
            Res = await Helpers.Get(address, token);

            if (Res.IsSuccessStatusCode)
            {
                int userId = Convert.ToInt32(Session["user"].ToString());
                address = "api/Servers/InsertServerBuild?BuildId=" + BuildId + "&ServerID=" + ServerId + "&UserId=" + userId;
                Res = await Helpers.Get(address, token);
                if (Res.IsSuccessStatusCode)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Rebuild(int id)
        {
            string token = Session["UserToken"].ToString();
            string addressUrl = "api/EmailMaster/GetEmails?id=" + Session["Organization"].ToString();
            List<EmailMaster> emails = new List<EmailMaster>();
            HttpResponseMessage Res = await Helpers.Get(addressUrl, token);
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
            addressUrl = "api/Servers/GetServerBuild?id=" + id;
            ServerBuild build = new ServerBuild();
            Res = await Helpers.Get(addressUrl, token);
            if (Res.IsSuccessStatusCode)
            {
                var Project = Res.Content.ReadAsStringAsync().Result;
                build = JsonConvert.DeserializeObject<ServerBuild>(Project);
            }
            string subject = build.BuildProject.Project.ProjectName + " " + build.Mejor_Version + "." + build.Minor_Version + "." + build.Build_Version;
            await Helpers.SendEmail(emailIds, subject, "New Server Build Started for the projct build" + subject);
            string address = ConfigurationManager.AppSettings["ServerBuildAPI"] + build.BuildProject.DownloadURL;
            Res = await Helpers.Get(address, token);
            if (Res.IsSuccessStatusCode)
            {
                int userid = Convert.ToInt32(Session["user"].ToString());
                ServerBuild temp = new ServerBuild { ServerBuildId = build.ServerBuildId, BuildId = build.BuildId, Build_Version = build.Build_Version, Mejor_Version = build.Mejor_Version, Minor_Version = build.Minor_Version, PublishDate = DateTime.Now.Date, Status = "queued", PublishedBy = userid, ServerId = build.ServerId };
                var stringContent = new StringContent(JsonConvert.SerializeObject(temp), Encoding.UTF8, "application/json");
                var addressUri = new Uri("api/Servers/UpdateServerBuild", UriKind.Relative);
                Res = Helpers.Post(addressUri, stringContent, token);
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
 
