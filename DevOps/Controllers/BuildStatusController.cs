using DevOps.Business.Interfaces;
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
    public class BuildStatusController : ApiController
    {
        private IBuildStatusManager _BuildStatusManager;
        public BuildStatusController(IBuildStatusManager buildStatusManager)
        {
            _BuildStatusManager = buildStatusManager;
        }

        public IHttpActionResult GetTotalSuccessfulBuilds(int id)
        {
            int total = _BuildStatusManager.TotalSuccessfulBuilds(id);
            return Ok(total);
        }

        public IHttpActionResult GetTotalFailedBuilds(int id)
        {
            int total = _BuildStatusManager.TotalFailedBuilds(id);
            return Ok(total);
        }

        public IHttpActionResult GetTotalQueuedBuilds(int id)
        {
            int total = _BuildStatusManager.TotalQueuedBuilds(id);
            return Ok(total);
        }

        public IHttpActionResult GetUserQueuedBuilds(int id)
        {
            int total = _BuildStatusManager.UserQueuedBuilds(id);
            return Ok(total);
        }

        public IHttpActionResult GetUserSuccessfulBuilds(int id)
        {
            int total = _BuildStatusManager.UserSuccessfulBuilds(id);
            return Ok(total);
        }

        public IHttpActionResult GetUserFailedBuilds(int id)
        {
            int total = _BuildStatusManager.UserFailedBuilds(id);
            return Ok(total);
        }
    }
}
