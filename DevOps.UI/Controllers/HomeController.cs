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
    [RoutePrefix("Dashboard")]
    public class HomeController : Controller
    {
        [Route("Dashboard")]
        public async Task<ActionResult> Index()
        {
            string address1;
            string token = Session["UserToken"].ToString();
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
            int projects = await Helpers.GetResponse(address1, token);
            ViewBag.Projects = projects;
            if (role != "User")
            {
                address1 = "api/Users/GetTotalUsers?id=" + gid;
                int users = await Helpers.GetResponse(address1, token);
                ViewBag.Users = users;
                int organizations;
                if (role == "Admin")
                {
                    address1 = "api/Organizations/GetTotalOrganizations";
                    organizations = await Helpers.GetResponse(address1, token);
                    ViewBag.Organizations = organizations;
                    address1 = "api/Organizations/GetNumberOfUserInOrganization";
                    HttpResponseMessage res = await Helpers.Get(address1, token);
                    List<Organization> userinOrg;
                    List<DataPoint> userDataPoints = new List<DataPoint>();

                    //Dummy Data
                    //userDataPoints.Add(new DataPoint("ABCD", 5));
                    //userDataPoints.Add(new DataPoint("XYZ", 3));
                    //userDataPoints.Add(new DataPoint("PQR", 7));

                    if (res.IsSuccessStatusCode)
                    {
                        var OrgResponse = res.Content.ReadAsStringAsync().Result;
                        userinOrg = JsonConvert.DeserializeObject<List<Organization>>(OrgResponse);
                        foreach (Organization org in userinOrg)
                        {
                            userDataPoints.Add(new DataPoint(org.Name, (double)org.Users.Count));
                        }
                    }

                    address1 = "api/Organizations/GetNumberOfProjectInOrganization";
                    res = await Helpers.Get(address1, token);
                    List<Organization> projectInOrg;
                    List<DataPoint> projectDataPoints = new List<DataPoint>();

                    //Dummy Data
                    //projectDataPoints.Add(new DataPoint("ABCD", 7));
                    //projectDataPoints.Add(new DataPoint("XYZ", 1));
                    //projectDataPoints.Add(new DataPoint("PQR", 5));

                    if (res.IsSuccessStatusCode)
                    {
                        var OrgResponse = res.Content.ReadAsStringAsync().Result;
                        projectInOrg = JsonConvert.DeserializeObject<List<Organization>>(OrgResponse);
                        foreach (Organization org in projectInOrg)
                        {
                            projectDataPoints.Add(new DataPoint(org.Name, (double)org.Projects.Count));
                        }
                    }

                    ViewBag.userDataPoints = JsonConvert.SerializeObject(userDataPoints);
                    ViewBag.projectDataPoints = JsonConvert.SerializeObject(projectDataPoints);
                }
                else
                {
                    address1 = "api/Servers/GetTotalServers?id=" + gid;
                    organizations = await Helpers.GetResponse(address1, token);
                    ViewBag.Organizations = organizations;
                }
                address1 = "api/SupportTicket/GetTotalSupportTickets?id=" + gid;
                int tickets = await Helpers.GetResponse(address1, token);
                ViewBag.Tickets = tickets;

                //Builds
                address1 = "api/Projects/GetTotalBuilds?id=" + gid;
                int builds = await Helpers.GetResponse(address1, token);
                ViewBag.Builds = builds;
                address1 = "api/Servers/GetTotalBuilds?id=" + gid;
                int sbuilds = await Helpers.GetResponse(address1, token);
                ViewBag.Sbuilds = sbuilds;
                address1 = "api/Package/GetTotalBuilds?id=" + gid;
                int release = await Helpers.GetResponse(address1, token);
                ViewBag.Release = release;
                List<DataPoint> dataPoints1 = new List<DataPoint>();
                dataPoints1.Add(new DataPoint("ProjectBuild", builds));
                dataPoints1.Add(new DataPoint("ServerBuild", sbuilds));
                dataPoints1.Add(new DataPoint("Release", release));
                ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);

                //SupportTicket Statistices
                address1 = "api/SupportTicket/GetTotalFixSupportTickets?id=" + gid;
                int fixticket = await Helpers.GetResponse(address1, token);
                ViewBag.FixTickets = fixticket;
                address1 = "api/SupportTicket/GetTotalNotFixedSupportTicket?id=" + gid;
                int NotFixed = await Helpers.GetResponse(address1, token);
                ViewBag.NotFixed = NotFixed;
                List<DataPoint> dataPoints3 = new List<DataPoint>();
                dataPoints3.Add(new DataPoint("FixTicket", fixticket));
                dataPoints3.Add(new DataPoint("NotFixTicket", NotFixed));
                ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);


                //Build Statistics
                address1 = "api/BuildStatus/GetTotalSuccessfulBuilds?id=" + gid;
                int success = await Helpers.GetResponse(address1, token);
                ViewBag.Success = success;
                address1 = "api/BuildStatus/GetTotalFailedBuilds?id=" + gid;
                int failed = await Helpers.GetResponse(address1, token);
                ViewBag.Failed = failed;
                address1 = "api/BuildStatus/GetTotalQueuedBuilds?id=" + gid;
                int queued = await Helpers.GetResponse(address1, token);
                ViewBag.Queued = queued;
                address1 = "api/Projects/GetRecentProjects?id=" + gid;
                ViewBag.TotalBuilds = success + failed + queued;
                List<DataPoint> dataPoints2 = new List<DataPoint>();
                dataPoints2.Add(new DataPoint("Success", success));
                dataPoints2.Add(new DataPoint("Failed", failed));
                dataPoints2.Add(new DataPoint("Queued", queued));
                ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
                HttpResponseMessage Res = await Helpers.Get(address1, token);
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
                    Res = await Helpers.Get(address1, token);
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
                    Res = await Helpers.Get(address1, token);
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
                //support tickets
                address1 = "api/SupportTicket/GetTotalFixSupportTicketsOfUsers?id=" + uid;
                int fixticket = await Helpers.GetResponse(address1, token);
                ViewBag.FixTickets = fixticket;
                address1 = "api/SupportTicket/GetTotalNotFixedSupportTicketOfUsers?id=" + uid;
                int NotFixed = await Helpers.GetResponse(address1, token);
                ViewBag.NotFixed = NotFixed;
                List<DataPoint> dataPoints1 = new List<DataPoint>();
                dataPoints1.Add(new DataPoint("FixTicket", fixticket));
                dataPoints1.Add(new DataPoint("NotFixTicket", NotFixed));
                ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);

                address1 = "api/SupportTicket/GetTotalUserSupportTickets?id=" + uid;
                int tickets = await Helpers.GetResponse(address1, token);
                ViewBag.Tickets = tickets;
                address1 = "api/Projects/GetTotalUserBuilds?id=" + uid;
                int builds = await Helpers.GetResponse(address1, token);
                ViewBag.Builds = builds;
                address1 = "api/BuildStatus/GetUserSuccessfulBuilds?id=" + uid;
                int success = await Helpers.GetResponse(address1, token);
                ViewBag.Success = success;
                address1 = "api/BuildStatus/GetUserFailedBuilds?id=" + uid;
                int failed = await Helpers.GetResponse(address1, token);
                ViewBag.Failed = failed;
                address1 = "api/BuildStatus/GetUserQueuedBuilds?id=" + uid;
                int queued = await Helpers.GetResponse(address1, token);
                ViewBag.Queued = queued;

                List<DataPoint> dataPoints2 = new List<DataPoint>();
                dataPoints2.Add(new DataPoint("Success", success));
                dataPoints2.Add(new DataPoint("Failed", failed));
                dataPoints2.Add(new DataPoint("Queued", queued));
                ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);

                //Builds
                address1 = "api/Projects/GetTotalBuilds?id=" + gid;
                int build = await Helpers.GetResponse(address1, token);
                ViewBag.Builds = builds;
                address1 = "api/Servers/GetTotalBuilds?id=" + gid;
                int sbuilds = await Helpers.GetResponse(address1, token);
                ViewBag.Sbuilds = sbuilds;
                address1 = "api/Package/GetTotalBuilds?id=" + gid;
                int release = await Helpers.GetResponse(address1, token);
                ViewBag.Release = release;
                List<DataPoint> dataPoints6 = new List<DataPoint>();
                dataPoints6.Add(new DataPoint("ProjectBuild", build));
                dataPoints6.Add(new DataPoint("ServerBuild", sbuilds));
                dataPoints6.Add(new DataPoint("Release", release));
                ViewBag.DataPoints6 = JsonConvert.SerializeObject(dataPoints6);

                address1 = "api/SupportTicket/GetUserSupportTickets?id=" + uid;
                HttpResponseMessage Res = await Helpers.Get(address1, token);
                List<SupportTicket> tickets1 = new List<SupportTicket>();
                if (Res.IsSuccessStatusCode)
                {
                    var BuildsResponse = Res.Content.ReadAsStringAsync().Result;
                    tickets1 = JsonConvert.DeserializeObject<List<SupportTicket>>(BuildsResponse);
                }
                TempData["tickets"] = tickets1;
                address1 = "api/Projects/GetUserBuilds?id=" + uid;
                Res = await Helpers.Get(address1, token);
                List<BuildProject> builds1 = new List<BuildProject>();
                if (Res.IsSuccessStatusCode)
                {
                    var BuildsResponse = Res.Content.ReadAsStringAsync().Result;
                    builds1 = JsonConvert.DeserializeObject<List<BuildProject>>(BuildsResponse);
                }
                TempData["builds"] = builds1;
            }
            if(HttpContext.Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }
    }
}
