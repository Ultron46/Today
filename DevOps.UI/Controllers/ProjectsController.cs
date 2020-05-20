using DevOps.Common;
using DevOps.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EmailMaster = DevOps.UI.Models.EmailMaster;

namespace DevOps.UI.Controllers
{
    public class ProjectsController : Controller
    {
        string baseUrl = Constants.baseurl;

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager", "User")]
        public ActionResult Projects()
        {
            return PartialView("Projects");
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager")]
        public ActionResult Registration()
        {
            return PartialView("Registration");
        }

        [HttpPost]
        [RoleAuth("Admin", "ReleaseManager")]
        public ActionResult Registration(Project project)
        {
            project.CreatedBy = Convert.ToInt32(Session["user"].ToString());
            project.CreatedDate = DateTime.Now;
            project.LastModifiedBy = Convert.ToInt32(Session["user"].ToString());
            project.LastModifiedDate = DateTime.Now;
            project.OrganisationId = Convert.ToInt32(Session["Organization"].ToString());
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringContent = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");
            var addressUri = new Uri("api/Projects/InsertProject", UriKind.Relative);
            HttpResponseMessage Res = client.PostAsync(addressUri, stringContent).Result;
            if (Res.IsSuccessStatusCode)
            {
                return Json(new { success = true }); ;
            }
            return Json(new { error = true });
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager")]
        public async Task<ActionResult> EditProject(int id)
        {
            Project projects = new Project();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/Projects/GetProject?id=" + id.ToString();
            HttpResponseMessage Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                projects = JsonConvert.DeserializeObject<Project>(MainMEnuResponse);
            }
            return PartialView(projects);
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager")]
        public async Task<ActionResult> Build(int projectId, int branchId)
        {
            int userId = Convert.ToInt32(Session["user"].ToString());
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
            addressUrl = "api/Projects/GetProject?id=" + projectId;
            Project project = new Project();
            Res = await client.GetAsync(addressUrl);
            if (Res.IsSuccessStatusCode)
            {
                var Project = Res.Content.ReadAsStringAsync().Result;
                project = JsonConvert.DeserializeObject<Project>(Project);
            }
            Helpers.SendEmail(emailIds, project.ProjectName, "New Build Started for the project");
            string address = ConfigurationManager.AppSettings["ProjectBuildAPI"] + project.SourceURL;
            Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                BuildProject build = new BuildProject { BuildBy = userId, BuildDate = DateTime.Now, DownloadURL = project.SourceURL, ProjectId = projectId, Status = "queued", BranchId = branchId};
                var stringContent = new StringContent(JsonConvert.SerializeObject(build), Encoding.UTF8, "application/json");
                var addressUri = new Uri("api/Projects/InsertBuildProject", UriKind.Relative);
                Res = client.PostAsync(addressUri, stringContent).Result;
                if (Res.IsSuccessStatusCode)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager","User")]
        public async Task<ActionResult> Builds()
        {
            string address;
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res;
            if (Session["Role"].ToString() == "Admin")
            {
                address = "api/Organizations/GetAllOrganization";
                List<Organization> organizations = new List<Organization>();
                Res = await client.GetAsync(address);
                if (Res.IsSuccessStatusCode)
                {
                    var Projects = Res.Content.ReadAsStringAsync().Result;
                    organizations = JsonConvert.DeserializeObject<List<Organization>>(Projects);
                }
                ViewBag.Orgs = organizations;
            }
            else
            {
                address = "api/Projects/GetOrganizationProject?id=" + Session["Organization"].ToString();
                List<Project> projects = new List<Project>(); 
                Res = await client.GetAsync(address);
                if (Res.IsSuccessStatusCode)
                {
                    var Projects = Res.Content.ReadAsStringAsync().Result;
                    projects = JsonConvert.DeserializeObject<List<Project>>(Projects);
                }
                ViewBag.Projects = projects;
            }
            return PartialView();
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager", "User")]
        public async Task<ActionResult> Rebuild(int id)
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
            addressUrl = "api/Projects/GetProjectBuild?id=" + id;
            BuildProject buildProject = new BuildProject();
            Res = await client.GetAsync(addressUrl);
            if (Res.IsSuccessStatusCode)
            {
                var BuildResponse = Res.Content.ReadAsStringAsync().Result;
                buildProject = JsonConvert.DeserializeObject<BuildProject>(BuildResponse);
            }
            Helpers.SendEmail(emailIds, buildProject.Project.ProjectName, "New Build Started for the project");
            string address = ConfigurationManager.AppSettings["ProjectBuildAPI"] + buildProject.Project.SourceURL;
            Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                int userid = Convert.ToInt32(Session["user"].ToString());
                BuildProject build = new BuildProject {BuildId = buildProject.BuildId, BuildBy = userid, BuildDate = DateTime.Now, DownloadURL =buildProject.Project.SourceURL, ProjectId = buildProject.ProjectId, Status = "queued", BranchId = buildProject.BranchId };
                var stringContent = new StringContent(JsonConvert.SerializeObject(build), Encoding.UTF8, "application/json");
                var addressUri = new Uri("api/Projects/UpdateBuildProject", UriKind.Relative);
                Res = client.PostAsync(addressUri, stringContent).Result;
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




    
