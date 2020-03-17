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
        // GET: Projects
        public async Task<ActionResult> Projects()
        {
            List<Project> projects = new List<Project>();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/Project/GetProjects");

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                projects = JsonConvert.DeserializeObject<List<Project>>(MainMEnuResponse);
            }

            ReturnArgs r = new ReturnArgs();
            r.status = 400;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         "Projects");
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                r.ViewString = sw.GetStringBuilder().ToString();
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> Registration()
        {
            ReturnArgs r = new ReturnArgs();
            r.status = 400;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         "Registration");
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                r.ViewString = sw.GetStringBuilder().ToString();
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Registration(Project project)
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
                return RedirectToAction("Index","Home");
            }

            return View("Registration", project);
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

            //ReturnArgs r = new ReturnArgs();
            //r.status = 400;
            //using (var sw = new StringWriter())
            //{
            //    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
            //                                                             "EditUser");
            //    var viewContext = new ViewContext(ControllerContext, viewResult.View,
            //                                 ViewData, TempData, sw);
            //    viewResult.View.Render(viewContext, sw);
            //    viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
            //    r.ViewString = sw.GetStringBuilder().ToString();
            //}
            return PartialView(projects);
        }



    }
}




    
