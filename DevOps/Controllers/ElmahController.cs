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
    public class ElmahController : ApiController
    {
        IElmahManager _elmahManager;

        public ElmahController(IElmahManager elmahManager)
        {
            _elmahManager = elmahManager;
        }

        [HttpGet]
        public IHttpActionResult GetElmahErrors()
        {
            List<ELMAH_Error> eLMAH_Errors = _elmahManager.GetELMAH_Errors();
            if (eLMAH_Errors != null)
                return Ok(eLMAH_Errors);
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetElmahErrorById(Guid id)
        {
            ELMAH_Error eLMAH_Error = _elmahManager.GerErroByID(id);
            if (eLMAH_Error != null)
                return Ok(eLMAH_Error);
            return NotFound();
        }
    }
}
