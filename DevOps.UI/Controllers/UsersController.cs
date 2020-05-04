using DevOps.Common;
using DevOps.UI.Models;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
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
    public class UsersController : Controller
    {
        private string baseUrl;
        private HttpClient client;
        private HttpResponseMessage Res;
        private string AddressUri;
        private StringContent StringContent;
        private Uri addressUri;
        public UsersController()
        {
            baseUrl = Constants.baseurl;
            client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
                    Value = ((int)i).ToString()
                });
            }
            ViewBag.Roles = roles;
            List<Organization> organisations = new List<Organization>();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["UserToken"].ToString());
            Res = await client.GetAsync("api/Organizations/GetAllOrganization");
            if (Res.IsSuccessStatusCode)
            {
                var OrganizationResponse = Res.Content.ReadAsStringAsync().Result;
                organisations = JsonConvert.DeserializeObject<List<Organization>>(OrganizationResponse);
            }
            var organizationTemp = organisations.Select(x => new SelectListItem { Text = x.Name, Value = x.OrganisationId.ToString() }).ToList();
            if (Session["Role"].ToString() != "Admin")
            {
                roles.Remove(roles.First());
                organizationTemp = organizationTemp.Where(x => x.Value == Session["Organization"].ToString()).ToList();
            }
            ViewBag.Organizations = organizationTemp;
            return PartialView("Registration");
        }

        [HttpGet]
        public async Task<ActionResult> Login(string returnurl)
        {
            if (!String.IsNullOrEmpty(returnurl))
            {
                return View();
            }
            HttpCookie cookie = Request.Cookies["DevOps"];
            if (cookie == null)
            {
                return View();
            }
            FormCollection form = new FormCollection();
            form["Email"] = cookie["username"];
            form["Password"] = cookie["password"];
            return await Login(form);
        }

        [HttpPost]
        public async Task<ActionResult> Login(FormCollection collection)
        {
            string cookiecheck = collection["Check"];
            User principal = null;
            AddressUri = "api/Users/GetAuthUser?email=" + collection["Email"] + "&password=" + collection["Password"];
            Res = await client.GetAsync(AddressUri);
            if (Res.IsSuccessStatusCode)
            {
                var UserResponse = Res.Content.ReadAsStringAsync().Result;
                principal = JsonConvert.DeserializeObject<User>(UserResponse);
            }
            if (principal != null)
            {
                int role = Convert.ToInt32(principal.RoleId);
                Session["Role"] = ((Enums.Roles)role).ToString();
                Session["Organization"] = principal.OrganisationId.ToString();
                Session["user"] = principal.UserId;
                Session["name"] = principal.Name;
                Session["Username"] = principal.Email;
                AddressUri = "api/Authorization/GetUserToken?id=" + principal.UserId.ToString();
                UserToken token1 = new UserToken();
                Res = await client.GetAsync(AddressUri);
                if (Res.IsSuccessStatusCode)
                {
                    var TokenResponse = Res.Content.ReadAsStringAsync().Result;
                    token1 = JsonConvert.DeserializeObject<UserToken>(TokenResponse);
                }
                if (String.IsNullOrEmpty(token1.Token))
                {
                    string token = Helpers.GenerateToken(principal.UserId, principal.Password);
                    UserToken userToken = new UserToken { UserID = principal.UserId, Token = token };
                    StringContent = new StringContent(JsonConvert.SerializeObject(userToken), Encoding.UTF8, "application/json");
                    addressUri = new Uri("api/Authorization/InsertToken", UriKind.Relative);
                    Res = client.PostAsync(addressUri, StringContent).Result;
                    if (!Res.IsSuccessStatusCode)
                    {
                        return View();
                    }
                    Session["UserToken"] = token;
                }
                else
                {
                    Session["UserToken"] = token1.Token;
                }
                List<MainMenu> mainMenus = new List<MainMenu>();
                List<SubMenu> subs = new List<SubMenu>();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["UserToken"].ToString());
                Res = await client.GetAsync("api/MainMenu/GetMainMenus");
                if (Res.IsSuccessStatusCode)
                {
                    var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                    mainMenus = JsonConvert.DeserializeObject<List<MainMenu>>(MainMEnuResponse);
                }
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Res = await client.GetAsync("api/SubMenu/GetSubMenus?id=" + role);
                if (Res.IsSuccessStatusCode)
                {
                    var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                    subs = JsonConvert.DeserializeObject<List<SubMenu>>(MainMEnuResponse);
                }
                var mainMenuIds = subs.Select(x => x.MainMenuId);
                mainMenus = mainMenus.Where(x => mainMenuIds.Contains(x.MainMenuId)).ToList();
                Session["Menu"] = mainMenus;
                Session["Subs"] = subs;
                if (!String.IsNullOrEmpty(cookiecheck))
                {
                    HttpCookie cookie = new HttpCookie("DevOps");
                    cookie["username"] = principal.Email;
                    cookie["password"] = principal.Password;
                    cookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cookie);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginError = "Not valid";
                return View("Login", collection);
            }
        }

        [HttpGet]
        public ActionResult Users()
        {
            return PartialView("UserTable");
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
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["UserToken"].ToString());
            if (Session["Role"].ToString() == "Admin")
            {
                Res = await client.GetAsync("api/Organizations/GetAllOrganization");
                if (Res.IsSuccessStatusCode)
                {
                    var OrganizationResponse = Res.Content.ReadAsStringAsync().Result;
                    organisations = JsonConvert.DeserializeObject<List<Organization>>(OrganizationResponse);
                }
            }
            if (!organisations.Where(x => x.OrganisationId.ToString() == Session["Organization"].ToString()).Any())
            {
                Organization organization = new Organization();
                AddressUri = "api/Organizations/GetOrganization?OrganisationId=" + Session["Organization"].ToString();
                Res = await client.GetAsync(AddressUri);
                if (Res.IsSuccessStatusCode)
                {
                    var OrganizationResponse = Res.Content.ReadAsStringAsync().Result;
                    organization = JsonConvert.DeserializeObject<Organization>(OrganizationResponse);
                }
                organisations.Add(organization);
            }
            if (Session["Role"].ToString() != "Admin")
            {
                roles.Remove(roles.First());
            }
            AddressUri = "api/Users/GetUser?userId=" + id.ToString();
            Res = await client.GetAsync(AddressUri);
            User user = new User();
            if (Res.IsSuccessStatusCode)
            {
                var UsersResponse = Res.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(UsersResponse);
            }
            ViewBag.Organizations = organisations;
            return PartialView(user);
        }

        public async Task<ActionResult> LogOut()
        {
            int id = Convert.ToInt32(Session["user"].ToString());
            AddressUri = "api/Authorization/DeleteToken?id=" + id.ToString();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["UserToken"].ToString());
            Res = await client.GetAsync(AddressUri);
            if (Res.IsSuccessStatusCode)
            {
                Session.Clear();
                Session.Abandon();
                HttpCookie aCookie = default;
                int i = 0;
                string cookieName = null;
                int limit = Request.Cookies.Count - 1;
                for (i = 0; i <= limit; i++)
                {
                    cookieName = Request.Cookies[i].Name;
                    aCookie = new HttpCookie(cookieName);
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);
                }
                return RedirectToAction("Login", new { returnurl = "LogOut" });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}