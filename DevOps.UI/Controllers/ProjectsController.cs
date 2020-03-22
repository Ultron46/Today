using DevOps.Common;
using DevOps.DataEntities.Models;
using DevOps.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
    }
}




    
