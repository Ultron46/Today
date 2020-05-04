using DevOps.Common;
using DevOps.DataEntities.Models;
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
        public ActionResult Projects()
        {
            return PartialView("Projects");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return PartialView("Registration");
        }

        [HttpPost]
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

        public async Task<ActionResult> Build(string sourceURL, int projectId, int userId)
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
            addressUrl = "api/Projects/GetProject?id=" + projectId;
            Project project = new Project();
            Res = await client.GetAsync(addressUrl);
            if (Res.IsSuccessStatusCode)
            {
                var Project = Res.Content.ReadAsStringAsync().Result;
                project = JsonConvert.DeserializeObject<Project>(Project);
            }
            Helpers.SendEmail(emailIds, project.ProjectName, "New Build Started for the project");
            string address = ConfigurationManager.AppSettings["ProjectBuildAPI"] + sourceURL;
            Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                client.DefaultRequestHeaders.Clear();
                address = "api/Projects/InsertBuildProject?sourceURL="+ sourceURL +"&projectId=" + projectId + "&userId=" + userId;
                Res = await client.GetAsync(address);
                if (Res.IsSuccessStatusCode)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Builds()
        {
            string address;
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if(Session["Role"].ToString() == "Admin")
            {
                address = "api/Projects/GetProjects";
            }
            else
            {
                address = "api/Projects/GetOrganizationProject?id=" + Session["Organization"].ToString();
            }
            List<Project> projects = new List<Project>();
            HttpResponseMessage Res = await client.GetAsync(address);
            if (Res.IsSuccessStatusCode)
            {
                var Projects = Res.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<List<Project>>(Projects);
            }
            ViewBag.Projects = projects;
            return PartialView();
        }
    }
}




    
