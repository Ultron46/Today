using DevOps.Common;
using DevOps.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DevOps.UI.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        [HttpGet]
        [RoleAuth("Admin")]
        public ActionResult Settings()
        {
            string ProjectBuildAPI = ConfigurationManager.AppSettings["ProjectBuildAPI"].ToString();
            string ServerBuildAPI = ConfigurationManager.AppSettings["ServerBuildAPI"].ToString();
            ViewBag.ProjectBuildAPI = ProjectBuildAPI;
            ViewBag.ServerBuildAPI = ServerBuildAPI;
            return PartialView();
        }

        [HttpGet]
        [RoleAuth("Admin")]
        public ActionResult EditSettings()
        {
            string ProjectBuildAPI = ConfigurationManager.AppSettings["ProjectBuildAPI"].ToString();
            string ServerBuildAPI = ConfigurationManager.AppSettings["ServerBuildAPI"].ToString();
            ViewBag.ProjectBuildAPI = ProjectBuildAPI;
            ViewBag.ServerBuildAPI = ServerBuildAPI;
            return PartialView();
        }

        [HttpPost]
        [RoleAuth("Admin")]
        public ActionResult UpdateSettings(string ProjectBuildAPI, string ServerBuildAPI)
        {
            Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            webConfigApp.AppSettings.Settings["ProjectBuildAPI"].Value = ProjectBuildAPI;
            webConfigApp.AppSettings.Settings["ServerBuildAPI"].Value = ServerBuildAPI;
            webConfigApp.Save();
            return Json(new { success = true}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager")]
        public ActionResult EmailMaster()
        {
            return PartialView();
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager")]
        public async Task<ActionResult> AddEmailMaster()
        {
            string address1;
            string token = Session["UserToken"].ToString();
            List<Organization> orgs = new List<Organization>();
            HttpResponseMessage Res;
            if (Session["Role"].ToString() == "Admin")
            {
                address1 = "api/Organizations/GetAllOrganization";
                Res = await Helpers.Get(address1, token);
                if (Res.IsSuccessStatusCode)
                {
                    var OrgResponse = Res.Content.ReadAsStringAsync().Result;
                    orgs = JsonConvert.DeserializeObject<List<Organization>>(OrgResponse);
                }
            }
            else
            {
                int id = Convert.ToInt32(Session["Organization"].ToString());
                address1 = "api/Organizations/GetOrganization?OrganisationId=" + id.ToString();
                Res = await Helpers.Get(address1, token);
                if (Res.IsSuccessStatusCode)
                {
                    var OrgResponse = Res.Content.ReadAsStringAsync().Result;
                    Organization organization = new Organization();
                    organization = JsonConvert.DeserializeObject<Organization>(OrgResponse);
                    orgs.Add(organization);
                }
            }
            ViewBag.Organizations = orgs;
            return PartialView();
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager")]
        public async Task<ActionResult> EditEmailMaster(int id)
        {
            string token = Session["UserToken"].ToString();
            string address = "api/EmailMaster/GetEmailMaster?id=" + id.ToString();
            EmailMaster email = new EmailMaster();
            HttpResponseMessage Res = await Helpers.Get(address, token);
            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                email = JsonConvert.DeserializeObject<EmailMaster>(MainMEnuResponse);
            }
            string address1;
            List<Organization> orgs = new List<Organization>();
            if (Session["Role"].ToString() == "Admin")
            {
                address1 = "api/Organizations/GetAllOrganization";
                Res = await Helpers.Get(address1, token);
                if (Res.IsSuccessStatusCode)
                {
                    var OrgResponse = Res.Content.ReadAsStringAsync().Result;
                    orgs = JsonConvert.DeserializeObject<List<Organization>>(OrgResponse);
                }
            }
            else
            {
                id = Convert.ToInt32(Session["Organization"].ToString());
                address1 = "api/Organizations/GetOrganization?OrganisationId=" + id.ToString();
                Res = await Helpers.Get(address1, token);
                if (Res.IsSuccessStatusCode)
                {
                    var OrgResponse = Res.Content.ReadAsStringAsync().Result;
                    Organization organization = new Organization();
                    organization = JsonConvert.DeserializeObject<Organization>(OrgResponse);
                    orgs.Add(organization);
                }
            }
            ViewBag.Organizations = orgs;
            return PartialView(email);
        }
    }
}