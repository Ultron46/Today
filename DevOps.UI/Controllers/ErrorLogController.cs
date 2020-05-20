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
        string baseUrl = Constants.baseurl;

        // GET: Elmah
        [HttpGet]
        public ActionResult ErrorLogList()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<ActionResult> ErrorLogDetail(Guid id)
        {
            var client = new HttpClient();
            ElmahError elmah = new ElmahError();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/Elmah/GetElmahErrorById/" + id.ToString();
            HttpResponseMessage Res = await client.GetAsync(address);
            if (Res.IsSuccessStatusCode)
            {

                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                elmah = JsonConvert.DeserializeObject<ElmahError>(MainMEnuResponse);

            }
            return PartialView(elmah);
        }
    }
}