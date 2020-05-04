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
    [EnableCorsAttribute("*","*","*")]
    public class EmailMasterController : ApiController
    {
        private IEmailMasterManager _emailMasterManager;
        public EmailMasterController(IEmailMasterManager emailMasterManager)
        {
            _emailMasterManager = emailMasterManager;
        }

        public IHttpActionResult GetEmails(int id)
        {
            List<EmailMaster> emailMasters = _emailMasterManager.GetEmails(id);
            if(emailMasters == null)
            {
                return NotFound();
            }
            return Ok(emailMasters);
        }
        public IHttpActionResult GetEmailMaster(int id)
        {
            EmailMaster emailMasters = _emailMasterManager.GetEmailMaster(id);
            if (emailMasters == null)
            {
                return NotFound();
            }
            return Ok(emailMasters);
        }
        [HttpPost]
        public IHttpActionResult InsertEmailMaster(EmailMaster email)
        {
            bool status = _emailMasterManager.InsertEmailMaster(email);
            return Ok(status);
        }
        [HttpPost]
        public IHttpActionResult UpdateEmailMaster(EmailMaster email)
        {
            bool status = _emailMasterManager.UpdateEmailMaster(email);
            return Ok(status);
        }
        [HttpPost]
        public IHttpActionResult DeleteEmailMaster(int id)
        {
            bool status = _emailMasterManager.DeleteEmailMaster(id);
            return Ok(status);
        }
    }
}
