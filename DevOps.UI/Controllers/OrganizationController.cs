﻿using DevOps.Common;
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
    [RoleAuth("Admin")]
    public class OrganizationController : Controller
    {
        [HttpGet]
        public ActionResult Registration()
        {
            return PartialView("Registration");
        }

        [HttpGet]
        public ActionResult Organization()
        {
            return PartialView("OrganizationTable");
        }

        [HttpGet]
        public async Task<ActionResult> EditOrganization(int id)
        {
            string token = Session["UserToken"].ToString();
            Organization organisations = new Organization();
            string address = "api/Organizations/GetOrganization?OrganisationId=" + id.ToString();
            HttpResponseMessage Res = await Helpers.Get(address, token);
            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                organisations = JsonConvert.DeserializeObject<Organization>(MainMEnuResponse);
            }
            return PartialView(organisations);
        }   
    }
}
