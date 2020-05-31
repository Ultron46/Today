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
    [RoleAuth("Admin")]
    public class ErrorLogController : Controller
    {
        // GET: Elmah
        [HttpGet]
        public ActionResult ErrorLog()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<ActionResult> ErrorLogDetail(Guid id)
        {
            ElmahError elmah = new ElmahError();
            string token = Session["UserToken"].ToString();
            string address = "api/Elmah/GetElmahErrorById/" + id.ToString();
            HttpResponseMessage Res = await Helpers.Get(address, token);
            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                elmah = JsonConvert.DeserializeObject<ElmahError>(MainMEnuResponse);
            }
            return PartialView(elmah);
        }
    }
}