using DevOps.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using DevOps.UI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.UI.Controllers
{
    

    public class SupportTicketController : Controller
    {
        string baseUrl = Constants.baseurl;

        [HttpGet]
        // GET: SupportTickets
        public ActionResult TicketGenerate()
        {
        
            return PartialView("TicketGenerate");

        }
        [HttpPost]
        public ActionResult TicketGenerate(SupportTicket supportTicket)
        {
            var status = 0;
            supportTicket.GeneratedBy = Convert.ToInt32(Session["user"].ToString());
            supportTicket.GeneratedDate = DateTime.Now;
            supportTicket.FixedBy = null;
            supportTicket.FixedDate = null;
            supportTicket.Status = "Paynding";
            //Session["username"] = User.Identity.Name;
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringContent = new StringContent(JsonConvert.SerializeObject(supportTicket), Encoding.UTF8, "application/json");
            var addressUri = new Uri("api/SupportTicket/InsertTicket", UriKind.Relative);
            HttpResponseMessage Res = client.PostAsync(addressUri, stringContent).Result;
            if (Res.IsSuccessStatusCode)
            {
                return Json(new { success = true }); ;
            }
            return Json(new { error = true });

        

        }
        [HttpGet]
        public ActionResult TicketManagement()
        {
            //var id=Convert.ToInt32(Session["user"].ToString());
            return PartialView("TicketManagement");
        }
        [HttpGet]
        public async Task<ActionResult> ShareSolution(int id)
        {
            SupportTicket supportTicket = new SupportTicket();
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/SupportTicket/GetSolution?TicketId=" + id.ToString();
            HttpResponseMessage Res = await client.GetAsync(address);
            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                supportTicket = JsonConvert.DeserializeObject<SupportTicket>(MainMEnuResponse);
            }
            ViewBag.Problem = supportTicket.Description;
            return PartialView(supportTicket);
        }


    }
}