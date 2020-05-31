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
        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager", "User")]
        // GET: SupportTickets
        public ActionResult TicketGenerate()
        {
            return PartialView("TicketGenerate");
        }

        [RoleAuth("Admin", "ReleaseManager", "User")]
        [HttpPost]
        public ActionResult TicketGenerate(SupportTicket supportTicket)
        {
            string token = Session["UserToken"].ToString();
            supportTicket.GeneratedBy = Convert.ToInt32(Session["user"].ToString());
            supportTicket.GeneratedDate = DateTime.Now;
            supportTicket.FixedBy = null;
            supportTicket.FixedDate = null;
            supportTicket.Status = "Paynding";
            var stringContent = new StringContent(JsonConvert.SerializeObject(supportTicket), Encoding.UTF8, "application/json");
            var addressUri = new Uri("api/SupportTicket/InsertTicket", UriKind.Relative);
            HttpResponseMessage Res = Helpers.Post(addressUri, stringContent, token);
            if (Res.IsSuccessStatusCode)
            {
                return Json(new { success = true }); ;
            }
            return Json(new { error = true });
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager", "User")]
        public ActionResult TicketManagement()
        {
            return PartialView("TicketManagement");
        }

        [HttpGet]
        [RoleAuth("Admin", "ReleaseManager")]
        public async Task<ActionResult> ShareSolution(int id)
        {
            string token = Session["UserToken"].ToString();
            SupportTicket supportTicket = new SupportTicket();
            string address = "api/SupportTicket/GetSolution?TicketId=" + id.ToString();
            HttpResponseMessage Res = await Helpers.Get(address, token);
            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                supportTicket = JsonConvert.DeserializeObject<SupportTicket>(MainMEnuResponse);
            }
            ViewBag.Problem = supportTicket.Description;
            ViewBag.To = supportTicket.User1.Email;
            return PartialView(supportTicket);
        }
    }
}