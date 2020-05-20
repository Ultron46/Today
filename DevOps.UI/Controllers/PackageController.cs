using DevOps.Common;
using DevOps.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace DevOps.UI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [RoleAuth("Admin", "ReleaseManager")]
    public class PackageController : Controller
    {
        string baseUrl = Constants.baseurl;

        [HttpGet]
        public ActionResult Registration()
        {
            return PartialView("Registration");
        }
        [HttpGet]
        public ActionResult Packages()
        {

            return PartialView("Packages");
        }
        [HttpGet]
        public async Task<ActionResult> EditPackage(int id)
        {
            PackageRelease packageRelease = new PackageRelease();
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/Package/GetPackage?id=" + id.ToString();
            HttpResponseMessage Res = await client.GetAsync(address);
            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                packageRelease = JsonConvert.DeserializeObject<PackageRelease>(MainMEnuResponse);
            }
            return PartialView(packageRelease);
        }

        [HttpGet]
        public async Task<ActionResult> Release()
        {

            string address;
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            address = "api/Projects/GetOrganizationProject?id=" + Session["Organization"].ToString();

            List<Project> projects = new List<Project>();
            HttpResponseMessage Res = await client.GetAsync(address);
            if (Res.IsSuccessStatusCode)
            {
                var Projects = Res.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<List<Project>>(Projects);
            }
            ViewBag.Projects = projects;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int id = Convert.ToInt32(Session["Organization"].ToString());
            address = "api/Servers/GetServerConfigs?Organization=" + id.ToString();
            List<ServerConfig> servers = new List<ServerConfig>();
            Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                var ServersResponse = Res.Content.ReadAsStringAsync().Result;
                servers = JsonConvert.DeserializeObject<List<ServerConfig>>(ServersResponse);
            }
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int uid = Convert.ToInt32(Session["Organization"].ToString());
            address = "api/Package/GetBuildVersion?id=" + uid.ToString();
            List<BuildProject> buildProjects = new List<BuildProject>();
            Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                var ServersResponse = Res.Content.ReadAsStringAsync().Result;
                buildProjects = JsonConvert.DeserializeObject<List<BuildProject>>(ServersResponse);
            }
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            address = "api/Package/GetAllPackages";
            List<PackageRelease> packageReleases = new List<PackageRelease>();
            Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                var ServersResponse = Res.Content.ReadAsStringAsync().Result;
                packageReleases = JsonConvert.DeserializeObject<List<PackageRelease>>(ServersResponse);
            }
            ViewBag.Version = buildProjects;
            ViewBag.Servers = servers;
            ViewBag.Projects = projects;
            ViewBag.Package = packageReleases;
            return PartialView();
        }
    }
}