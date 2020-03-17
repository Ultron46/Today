using DevOps.Common;
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
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace DevOps.UI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class OrganizationController : Controller
    {
       
        string baseUrl = Constants.baseurl;
        // GET: User
        //[HttpGet]
        //public async Task<ActionResult> Index(string sortName, int? page)
        //{
        //    List<User> users = new List<User>();
        //    var client = new HttpClient();

        //    client.BaseAddress = new Uri(baseUrl);

        //    client.DefaultRequestHeaders.Clear();

        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    HttpResponseMessage Res = await client.GetAsync("api/Users/GetAllUsers");

        //    if (Res.IsSuccessStatusCode)
        //    {
        //        var userResponse = Res.Content.ReadAsStringAsync().Result;

        //        users = JsonConvert.DeserializeObject<List<User>>(userResponse);
        //    }


        //    var user = from u in users select u;

        //    ViewBag.CurrentSort = sortName;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortName) ? "name_desc" : "";
        //    //var user = new List<User>();
        //    switch (sortName)
        //    {
        //        case "name_desc":
        //            user = user.OrderByDescending(s => s.Name);
        //            break;
        //        default:
        //            user = user.OrderBy(s => s.Name);
        //            break;
        //    }
        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);
        //    return View(user.ToPagedList(pageNumber, pageSize));


        //    //int pageSize = 3;
        //    //int pageNumber = (page ?? 1);
        //    //var xya = users.ToPagedList(pageNumber, pageSize);
        //    //var user = from u in xya select u;

        //    //ViewBag.CurrentSort = sortName;
        //    //ViewBag.NameSortParm = String.IsNullOrEmpty(sortName) ? "name_desc" : "";
        //    ////var user = new List<User>();
        //    //switch (sortName)
        //    //{
        //    //    case "name_desc":
        //    //        user = user.OrderByDescending(s => s.Name);
        //    //        break;
        //    //    default:
        //    //        user = user.OrderBy(s => s.Name);
        //    //        break;
        //    //}

        //    //return View(user);

        //    //return View(users);
        //}

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
        public async Task<ActionResult> Registration(Organization organization)
        {
            // test Comment
            List<Organization> organizations = new List<Organization>();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/Organizations/GetAllOrganization");

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                organizations = JsonConvert.DeserializeObject<List<Organization>>(MainMEnuResponse);
            }

            client.DefaultRequestHeaders.Clear();


            var stringContent = new StringContent(JsonConvert.SerializeObject(organization), Encoding.UTF8, "application/json");

            var addressUri = new Uri("api/Organizations/InsertOrganization", UriKind.Relative);
            Res = client.PostAsync(addressUri, stringContent).Result;

            if (Res.IsSuccessStatusCode)
            {
                return View("OrganizationTable");
            }

            return View("Registration", organization);
        }




        [HttpGet]
        public async Task<ActionResult> Organization()
        {
            List<Organization> organizations = new List<Organization>();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/Organizations/GetAllOrganization");

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                organizations = JsonConvert.DeserializeObject<List<Organization>>(MainMEnuResponse);
            }
          
            ReturnArgs r = new ReturnArgs();
            r.status = 400;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         "OrganizationTable");
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                r.ViewString = sw.GetStringBuilder().ToString();
            }
            return Json(r, JsonRequestBehavior.AllowGet);
            //else
            //{
            //    ViewBag.LoginError = "Not valid";
            //    return View("Login", user);
            //}


        }

        [HttpGet]
        public async Task<ActionResult> EditOrganization(int id)
        {
            Organization organisations = new Organization();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/Organizations/GetOrganization?OrganisationId=" + id.ToString();
            HttpResponseMessage Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                organisations = JsonConvert.DeserializeObject<Organization>(MainMEnuResponse);
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
            return PartialView(organisations);
        }


        
    }
}
