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

        [HttpGet]
        public async Task<ActionResult> Registration()
        {
            return PartialView("Registration");
        }

        //[HttpPost]
        //public async Task<ActionResult> Registration(Organization organization)
        //{
        //    List<Organization> organizations = new List<Organization>();
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri(baseUrl);
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage Res = await client.GetAsync("api/Organizations/GetAllOrganization");
        //    if (Res.IsSuccessStatusCode)
        //    {
        //        var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
        //        organizations = JsonConvert.DeserializeObject<List<Organization>>(MainMEnuResponse);
        //    }
        //    client.DefaultRequestHeaders.Clear();
        //    var stringContent = new StringContent(JsonConvert.SerializeObject(organization), Encoding.UTF8, "application/json");
        //    var addressUri = new Uri("api/Organizations/InsertOrganization", UriKind.Relative);
        //    Res = client.PostAsync(addressUri, stringContent).Result;
        //    if (Res.IsSuccessStatusCode)
        //    {
        //        return View("OrganizationTable");
        //    }
        //    return View("Registration", organization);
        //}

        [HttpGet]
        public async Task<ActionResult> Organization()
        {
            return PartialView("OrganizationTable");
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
            return PartialView(organisations);
        }   
    }
}
