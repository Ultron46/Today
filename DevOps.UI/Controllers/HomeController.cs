using DevOps.Common;
using DevOps.UI;
using DevOps.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DevOps.Controllers
{
    [RoleAuth("Admin", "ReleaseManager", "User")]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string address1;
            string role = Session["Role"].ToString();
            int uid = Convert.ToInt32(Session["user"].ToString());
            int gid = 0;
            if (role == "Admin")
            {
                gid = 0;
            }
            else
            {
                gid = Convert.ToInt32(Session["Organization"].ToString());
            }
            address1 = "api/Projects/GetTotalProjects?id=" + gid;
            int projects = await Helpers.GetResponse(address1);
            ViewBag.Projects = projects;
            if (role != "User")
            {
                address1 = "api/Users/GetTotalUsers?id=" + gid;
                int users = await Helpers.GetResponse(address1);
                ViewBag.Users = users;
                int organizations;
                if (role == "Admin")
                {
                    address1 = "api/Organizations/GetTotalOrganizations";
                    organizations = await Helpers.GetResponse(address1);
                    ViewBag.Organizations = organizations;
                }
                else
                {
                    address1 = "api/Servers/GetTotalServers?id=" + gid;
                    organizations = await Helpers.GetResponse(address1);
                    ViewBag.Organizations = organizations;
                }
                address1 = "api/SupportTicket/GetTotalSupportTickets?id=" + gid;
                int tickets = await Helpers.GetResponse(address1);
                ViewBag.Tickets = tickets;
                address1 = "api/Projects/GetTotalBuilds?id=" + gid;
                int builds = await Helpers.GetResponse(address1);
                ViewBag.Builds = builds;
                address1 = "api/Servers/GetTotalBuilds?id=" + gid;
                int sbuilds = await Helpers.GetResponse(address1);
                ViewBag.Sbuilds = sbuilds;
                address1 = "api/Package/GetTotalBuilds?id=" + gid;
                int release = await Helpers.GetResponse(address1);
                ViewBag.Release = release;
                ViewBag.TotalBuilds = builds + sbuilds + release;
                address1 = "api/BuildStatus/GetTotalSuccessfulBuilds?id=" + gid;
                int success = await Helpers.GetResponse(address1);
                ViewBag.Success = success;
                address1 = "api/BuildStatus/GetTotalFailedBuilds?id=" + gid;
                int failed = await Helpers.GetResponse(address1);
                ViewBag.Failed = failed;
                address1 = "api/BuildStatus/GetTotalQueuedBuilds?id=" + gid;
                int queued = await Helpers.GetResponse(address1);
                ViewBag.Queued = queued;
                address1 = "api/Projects/GetRecentProjects?id=" + gid;
                HttpResponseMessage Res = await Helpers.Get(address1);
                List<Project> projects1 = new List<Project>();
                if (Res.IsSuccessStatusCode)
                {
                    var BuildsResponse = Res.Content.ReadAsStringAsync().Result;

                    projects1 = JsonConvert.DeserializeObject<List<Project>>(BuildsResponse);
                }
                TempData["projects"] = projects1;
                if (gid == 0)
                {
                    address1 = "api/Organizations/GetRecentOrganizations";
                    Res = await Helpers.Get(address1);
                    List<Organization> organizations1 = new List<Organization>();
                    if (Res.IsSuccessStatusCode)
                    {
                        var BuildsResponse = Res.Content.ReadAsStringAsync().Result;

                        organizations1 = JsonConvert.DeserializeObject<List<Organization>>(BuildsResponse);
                    }
                    TempData["orgs"] = organizations1;
                }
                else
                {
                    address1 = "api/Users/GetRecentUsers?id=" + gid;
                    Res = await Helpers.Get(address1);
                    List<User> users1 = new List<User>();
                    if (Res.IsSuccessStatusCode)
                    {
                        var BuildsResponse = Res.Content.ReadAsStringAsync().Result;

                        users1 = JsonConvert.DeserializeObject<List<User>>(BuildsResponse);
                    }
                    TempData["users"] = users1;
                }
            }
            else
            {
                address1 = "api/SupportTicket/GetTotalUserSupportTickets?id=" + uid;
                int tickets = await Helpers.GetResponse(address1);
                ViewBag.Tickets = tickets;
                address1 = "api/Projects/GetTotalUserBuilds?id=" + uid;
                int builds = await Helpers.GetResponse(address1);
                ViewBag.Builds = builds;
                address1 = "api/BuildStatus/GetUserSuccessfulBuilds?id=" + uid;
                int success = await Helpers.GetResponse(address1);
                ViewBag.Success = success;
                address1 = "api/BuildStatus/GetUserFailedBuilds?id=" + uid;
                int failed = await Helpers.GetResponse(address1);
                ViewBag.Failed = failed;
                address1 = "api/BuildStatus/GetUserQueuedBuilds?id=" + uid;
                int queued = await Helpers.GetResponse(address1);
                ViewBag.Queued = queued;
                address1 = "api/SupportTicket/GetUserSupportTickets?id=" + uid;
                HttpResponseMessage Res = await Helpers.Get(address1);
                List<SupportTicket> tickets1 = new List<SupportTicket>();
                if (Res.IsSuccessStatusCode)
                {
                    var BuildsResponse = Res.Content.ReadAsStringAsync().Result;

                    tickets1 = JsonConvert.DeserializeObject<List<SupportTicket>>(BuildsResponse);
                }
                TempData["tickets"] = tickets1;
                address1 = "api/Projects/GetUserBuilds?id=" + uid;
                Res = await Helpers.Get(address1);
                List<BuildProject> builds1 = new List<BuildProject>();
                if (Res.IsSuccessStatusCode)
                {
                    var BuildsResponse = Res.Content.ReadAsStringAsync().Result;

                    builds1 = JsonConvert.DeserializeObject<List<BuildProject>>(BuildsResponse);
                }
                TempData["builds"] = builds1;
            }
            return View();
        }
    }
}
