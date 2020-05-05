using DevOps.Business.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DevOps.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]

    public class SupportTicketController : ApiController
    {
        private ISupportTicketManager _supportTicketManager;
        public SupportTicketController() { }
        public SupportTicketController(ISupportTicketManager supportTicketManager)
        {
            _supportTicketManager = supportTicketManager;
        }


        public IHttpActionResult GetAllTicket(string role, int organization, int id)
        {
            var ticket = _supportTicketManager.GetAllTicket();

            //int roles = Convert.ToInt32(role);
            //if (role != "Admin")
            //{
            //    ticket = ticket.Where(x => x.User1.OrganisationId == organization).ToList();
            //}


            if (role == "ReleaseManager")
            {
                ticket = ticket.Where(x => x.User1.OrganisationId == organization).ToList();
            }
            else if (role == "Admin")
            {
                ticket = ticket.ToList();
            }
            else
            {
                ticket = ticket.Where(x => x.User1.UserId == id).ToList();
            }
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost]
        public IHttpActionResult InsertTicket(SupportTicket supportTicket)
        {
            bool status = _supportTicketManager.InsertTicket(supportTicket);
            if (status)
                return Ok(status);
            return NotFound();
        }

        public IHttpActionResult GetSolution(int TicketId)
        {
            var supportTicket = _supportTicketManager.GetSolution(TicketId);
            if (supportTicket == null)
            {
                return NotFound();
            }
            return Ok(supportTicket);
        }
        [HttpGet]
        public IHttpActionResult GetTicket(int id, int tid)
        {
            bool status = _supportTicketManager.GetTicket(id, tid);
            if (status)
            {
                return Ok(status);
            }
            return NotFound();
        }

    }
}



