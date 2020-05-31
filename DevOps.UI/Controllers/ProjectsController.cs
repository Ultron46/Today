using DevOps.Common;
using DevOps.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
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
        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager", "User")]
        public ActionResult Projects()
        {
            return PartialView("Projects");
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager")]
        public async Task<ActionResult> Registration()
        {
            string address;
            string token = Session["UserToken"].ToString();
            HttpResponseMessage Res;
            if (Session["Role"].ToString() == "Admin")
            {
                address = "api/Organizations/GetAllOrganization";
                List<Organization> organizations = new List<Organization>();
                Res = await Helpers.Get(address, token);
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
        [RoleAuth("Admin", "ReleaseManager")]
        public ActionResult Registration(Project project)
        {
            project.CreatedBy = Convert.ToInt32(Session["user"].ToString());
            project.CreatedDate = DateTime.Now;
            project.LastModifiedBy = Convert.ToInt32(Session["user"].ToString());
            project.LastModifiedDate = DateTime.Now;
            project.OrganisationId = project.OrganisationId == null ? Convert.ToInt32(Session["Organization"].ToString()) : project.OrganisationId;
            string token = Session["UserToken"].ToString();
            var stringContent = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");
            var addressUri = new Uri("api/Projects/InsertProject", UriKind.Relative);
            HttpResponseMessage Res = Helpers.Post(addressUri, stringContent, token);
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
            string token = Session["UserToken"].ToString();
            string address = "api/Projects/GetProject?id=" + id.ToString();
            HttpResponseMessage Res = await Helpers.Get(address, token);
            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<Project>(MainMEnuResponse);
            }
            return PartialView(projects);
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager","User")]
        public async Task<ActionResult> Build(int projectId, int branchId)
        {
            int userId = Convert.ToInt32(Session["user"].ToString());
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
            addressUrl = "api/Projects/GetProject?id=" + projectId;
            Project project = new Project();
            Res = await Helpers.Get(addressUrl, token);
            if (Res.IsSuccessStatusCode)
            {
                var Project = Res.Content.ReadAsStringAsync().Result;
                project = JsonConvert.DeserializeObject<Project>(Project);
            }
            await Helpers.SendEmail(emailIds, project.ProjectName, "New Build Started for the project");
            string address = ConfigurationManager.AppSettings["ProjectBuildAPI"] + project.SourceURL;
            Res = await Helpers.Get(address, token);

            if (Res.IsSuccessStatusCode)
            {
                BuildProject build = new BuildProject { BuildBy = userId, BuildDate = DateTime.Now, ProjectId = projectId, Status = "queued", BranchId = branchId};
                var stringContent = new StringContent(JsonConvert.SerializeObject(build), Encoding.UTF8, "application/json");
                var addressUri = new Uri("api/Projects/InsertBuildProject", UriKind.Relative);
                Res = Helpers.Post(addressUri, stringContent, token);
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
            string token = Session["UserToken"].ToString();
            HttpResponseMessage Res;
            if (Session["Role"].ToString() == "Admin")
            {
                address = "api/Organizations/GetAllOrganization";
                List<Organization> organizations = new List<Organization>();
                Res = await Helpers.Get(address, token);
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
                Res = await Helpers.Get(address, token);
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
            addressUrl = "api/Projects/GetProjectBuild?id=" + id;
            BuildProject buildProject = new BuildProject();
            Res = await Helpers.Get(addressUrl, token);
            if (Res.IsSuccessStatusCode)
            {
                var BuildResponse = Res.Content.ReadAsStringAsync().Result;
                buildProject = JsonConvert.DeserializeObject<BuildProject>(BuildResponse);
            }
            await Helpers.SendEmail(emailIds, buildProject.Project.ProjectName, "New Build Started for the project");
            string address = ConfigurationManager.AppSettings["ProjectBuildAPI"] + buildProject.Project.SourceURL;
            Res = await Helpers.Get(address, token);
            if (Res.IsSuccessStatusCode)
            {
                int userid = Convert.ToInt32(Session["user"].ToString());
                BuildProject build = new BuildProject {BuildId = buildProject.BuildId, BuildBy = userid, BuildDate = DateTime.Now, DownloadURL = Server.MapPath("~") + "Builds\\", ProjectId = buildProject.ProjectId, Status = "queued", BranchId = buildProject.BranchId };
                var stringContent = new StringContent(JsonConvert.SerializeObject(build), Encoding.UTF8, "application/json");
                var addressUri = new Uri("api/Projects/UpdateBuildProject", UriKind.Relative);
                Res = Helpers.Post(addressUri, stringContent, token);
                if (Res.IsSuccessStatusCode)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public void DownloadBuildProject(string fileName)
        {
            try
            {
                string path = Server.MapPath("~") + "Builds\\" + fileName;
                path = path.Replace("DevOps.UI", "DevOps");
                path = @path;
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "filename=" + fileName);
                Response.TransmitFile(path);
                Response.Flush();
                Response.End();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [HttpGet]
        public void DownloadReleaseProject(string fileName)
        {
            try
            {
                string path = Server.MapPath("~") + "Releases\\" + fileName;
                path = path.Replace("DevOps.UI", "DevOps");
                path = @path;
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "filename=" + fileName);
                Response.TransmitFile(path);
                Response.Flush();
                Response.End();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}




    
