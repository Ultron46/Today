using DevOps.Common;
using DevOps.UI.Models;
using Newtonsoft.Json;
using PagedList;
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
    [EnableCorsAttribute("*","*","*")]
    public class UsersController : Controller
    {
        string baseUrl = Constants.baseurl;
        // GET: User
        [HttpGet]
        public async Task<ActionResult> Index(string sortName,int? page)
        {
            List<User> users = new List<User>();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/Users/GetAllUsers");

            if (Res.IsSuccessStatusCode)
            {
                var userResponse = Res.Content.ReadAsStringAsync().Result;

                users = JsonConvert.DeserializeObject<List<User>>(userResponse);
            }


            var user = from u in users select u;

            ViewBag.CurrentSort = sortName;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortName) ? "name_desc" : "";
            //var user = new List<User>();
            switch (sortName)
            {
                case "name_desc":
                    user = user.OrderByDescending(s => s.Name);
                    break;
                default:
                    user = user.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(user.ToPagedList(pageNumber, pageSize));


            //int pageSize = 3;
            //int pageNumber = (page ?? 1);
            //var xya = users.ToPagedList(pageNumber, pageSize);
            //var user = from u in xya select u;

            //ViewBag.CurrentSort = sortName;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortName) ? "name_desc" : "";
            ////var user = new List<User>();
            //switch (sortName)
            //{
            //    case "name_desc":
            //        user = user.OrderByDescending(s => s.Name);
            //        break;
            //    default:
            //        user = user.OrderBy(s => s.Name);
            //        break;
            //}

            //return View(user);

            //return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Registration()
        {
            Array values = Enum.GetValues(typeof(Enums.Roles));
            List<SelectListItem> roles = new List<SelectListItem>(values.Length);

            foreach (var i in values)
            {
                roles.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(Enums.Roles), i),
                    Value =((int)i).ToString()
                });
            }
            ViewBag.Roles = roles;
            List<Organization> organisations = new List<Organization>();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/Organizations/GetAllOrganization");

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                organisations = JsonConvert.DeserializeObject<List<Organization>>(MainMEnuResponse);
            }
            var organizationTemp = organisations.Select(x => new SelectListItem { Text = x.Name, Value = x.OrganisationId.ToString() }).ToList();
            if (Session["Role"].ToString() != "Admin")
            {
                roles.Remove(roles.First());
                organizationTemp = organizationTemp.Where(x => x.Value == Session["Organization"].ToString()).ToList();
            }
            ViewBag.Organizations = organizationTemp;
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
            return Json(r,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Registration(User user)
        {
            // test Comment
            List<User> users = new List<User>();
            var client = new HttpClient();
            
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Users/GetAllUsers");

                if (Res.IsSuccessStatusCode)
                {
                    var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                    users = JsonConvert.DeserializeObject<List<User>>(MainMEnuResponse);
                }
            bool check = true;
            if(users.Any(x => x.Email == user.Email))
            {
                ViewBag.EmailError = "email id already exists";
                check = false;
            }
            if (users.Any(x => x.Phone == user.Phone))
            {
                ViewBag.PhoneError = "Mobile Number is already exists";
                check = false;
            }
            if(check)
            {
                client.DefaultRequestHeaders.Clear();

                user.RegistrationDate = DateTime.Now;
                var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                var addressUri = new Uri("api/Users/InsertUser", UriKind.Relative);
                Res = client.PostAsync(addressUri, stringContent).Result;

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index","Home");
                }
            }

            return View("Registration", user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            List<User> users = new List<User>();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/Users/GetAllUsers");

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                users = JsonConvert.DeserializeObject<List<User>>(MainMEnuResponse);
            }
            User principal = users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            //bool check = true;
            if (principal != null)
            {
                int role = Convert.ToInt32(principal.RoleId);
                Session["Role"] = ((Enums.Roles)role).ToString();
                Session["Organization"] = principal.OrganisationId.ToString();
                Session["user"] = principal.UserId;
                List<MainMenu> mainMenus = new List<MainMenu>();
                List<SubMenu> subs = new List<SubMenu>();

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Res = await client.GetAsync("api/MainMEnu/GetMainMEnus");

                if (Res.IsSuccessStatusCode)
                {
                    var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                    mainMenus = JsonConvert.DeserializeObject<List<MainMenu>>(MainMEnuResponse);
                }

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Res = await client.GetAsync("api/SubMenu/GetSubMenus?id="+role);

                if (Res.IsSuccessStatusCode)
                {
                    var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                    subs = JsonConvert.DeserializeObject<List<SubMenu>>(MainMEnuResponse);
                }
                var mainMenuIds = subs.Select(x => x.MainMenuId);
                mainMenus = mainMenus.Where(x => mainMenuIds.Contains(x.MainMenuId)).ToList();
                Session["Menu"] = mainMenus;
                Session["Subs"] = subs;

                return RedirectToAction("Index","Home");
               
            }
            else
            {
                ViewBag.LoginError = "Not valid";
                return View("Login", user);
            }
           

        }

       
        [HttpGet]
        public async Task<ActionResult> Users()
        {
            List<User> users = new List<User>();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/Users/GetAllUsers");

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                users = JsonConvert.DeserializeObject<List<User>>(MainMEnuResponse);
            }

            if(Session["Role"].ToString() != "Admin")
            {
                users = users.Where(x => x.OrganisationId == Convert.ToInt32(Session["Organization"].ToString())).ToList();
            }
            //User principal = users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            ////bool check = true;
            //if (principal != null)
            //{
            //    int role = Convert.ToInt32(principal.RoleId);
            //    Session["Role"] = ((Enums.Roles)role).ToString();
            //    Session["Organization"] = principal.OrganisationId.ToString();
            //List<MainMenu> mainMenus = new List<MainMenu>();
            // List<SubMenu> subs = new List<SubMenu>();

            //    client.DefaultRequestHeaders.Clear();

            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    Res = await client.GetAsync("api/MainMEnu/GetMainMEnus");

            //    if (Res.IsSuccessStatusCode)
            //    {
            //        var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

            //        mainMenus = JsonConvert.DeserializeObject<List<MainMenu>>(MainMEnuResponse);
            //    }

            //    client.DefaultRequestHeaders.Clear();

            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    //Res = await client.GetAsync("api/SubMenu/GetSubMenus?id=" + role);

            //    if (Res.IsSuccessStatusCode)
            //    {
            //        var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

            //        subs = JsonConvert.DeserializeObject<List<SubMenu>>(MainMEnuResponse);
            //    }
            //    var mainMenuIds = subs.Select(x => x.MainMenuId);
            //    mainMenus = mainMenus.Where(x => mainMenuIds.Contains(x.MainMenuId)).ToList();
            //    Session["Menu"] = mainMenus;
            //    Session["Subs"] = subs;
            ReturnArgs r = new ReturnArgs();
            r.status = 400;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         "UserTable");
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
        public async Task<ActionResult> EditUser(int id)
        {
            Array values = Enum.GetValues(typeof(Enums.Roles));
            List<SelectListItem> roles = new List<SelectListItem>(values.Length);

            foreach (var i in values)
            {
                roles.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(Enums.Roles), i),
                    Value = ((int)i).ToString()
                });
            }
            ViewBag.Roles = roles;
            List<Organization> organisations = new List<Organization>();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/Organizations/GetAllOrganization");

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                organisations = JsonConvert.DeserializeObject<List<Organization>>(MainMEnuResponse);
            }
            var organizationTemp = organisations.Select(x => new SelectListItem { Text = x.Name, Value = x.OrganisationId.ToString() }).ToList();
            if (Session["Role"].ToString() != "Admin")
            {
                roles.Remove(roles.First());
                organizationTemp = organizationTemp.Where(x => x.Value == Session["Organization"].ToString()).ToList();
            }
            ViewBag.Organizations = organizationTemp;
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string userurl = "api/Users/GetUser?userId=" + id.ToString();
            Res = await client.GetAsync(userurl);
            User user = new User();
            if (Res.IsSuccessStatusCode)
            {
               
                   

                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                user = JsonConvert.DeserializeObject<User>(MainMEnuResponse);
                //return View("Index");
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
            return PartialView(user);
        }


       

    }
}