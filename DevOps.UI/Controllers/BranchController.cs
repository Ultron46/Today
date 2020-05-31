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
using System.Web.Mvc;

namespace DevOps.UI.Controllers
{
    public class BranchController : Controller
    {
        // GET: Branch
        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager", "User")]
        public async Task<ActionResult> Branch()
        {
            List<Project> projects = new List<Project>();
            string address1;
            string token = Session["UserToken"].ToString();
            if (Session["Role"].ToString() == "Admin")
            {
                address1 = "api/Projects/GetProjects?id=" + 0;
            }
            else
            {
                int id = Convert.ToInt32(Session["Organization"].ToString());
                address1 = "api/Projects/GetProjects?id=" + id.ToString();
            }
            HttpResponseMessage Res = await Helpers.Get(address1, token);
            if (Res.IsSuccessStatusCode)
            {
                var BuildsResponse = Res.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<List<Project>>(BuildsResponse);
            }
            ViewBag.Projects = projects;
            return PartialView();
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager", "User")]
        public ActionResult InsertBranch(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager", "User")]
        public async Task<ActionResult> EditBranch(int id)
        {
            string address1;
            string token = Session["UserToken"].ToString();
            address1 = "api/Branches/GetBranch?id=" + id;
            HttpResponseMessage Res = await Helpers.Get(address1, token);
            Branch branch = new Branch();
            if (Res.IsSuccessStatusCode)
            {
                var BuildsResponse = Res.Content.ReadAsStringAsync().Result;
                branch = JsonConvert.DeserializeObject<Branch>(BuildsResponse);
            }
            return PartialView(branch);
        }
    }
}