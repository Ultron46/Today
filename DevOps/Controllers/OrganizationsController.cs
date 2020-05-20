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
    public class OrganizationsController : ApiController
    {
        private IOrganizationsManager _organizationsManager;
        public OrganizationsController() { }
        public OrganizationsController(IOrganizationsManager organizationsManager)
        {
            _organizationsManager = organizationsManager;
        }



        //public IHttpActionResult GetOrganizations()
        //{
        //    List<Organisation> organisations = _organizationsManager.GetOrganisations();
        //    if(organisations == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(organisations);
        //}

        public IHttpActionResult GetAllOrganization()
        {
            var organizations = _organizationsManager.GetAllOrganization();
            if (organizations == null)
            {
                return NotFound();
            }
            return Ok(organizations);
        }

        public IHttpActionResult GetOrganization(int OrganisationId)
        {
            var organisation = _organizationsManager.GetOrganization(OrganisationId);
            if (organisation == null)
            {
                return NotFound();
            }
            return Ok(organisation);
        }

        [HttpPost]
        public IHttpActionResult InsertOrganization(Organisation organisation)
        {
            bool status = _organizationsManager.InsertOrganization(organisation);
            if (status)
                return Ok(status);
            return NotFound();
        }

        [HttpPost]
        public bool UpdateOrganization(Organisation organisation)
        {
            return _organizationsManager.UpdateOrganization(organisation);
        }

        [HttpPost]
        public IHttpActionResult DeleteOrganization(int id)
        {
            bool status = _organizationsManager.DeleteOrganization(id);
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        public IHttpActionResult GetTotalOrganizations()
        {
            int total = _organizationsManager.TotalOrganizations();
            return Ok(total);
        }

        public IHttpActionResult GetRecentOrganizations()
        {
            List<Organisation> organisations = _organizationsManager.GetRecentOrganizations();
            return Ok(organisations);
        }
    }
}
