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
    public class BranchesController : ApiController
    {
        private IBranchManager _BranchManager;
        public BranchesController(IBranchManager branchManager)
        {
            _BranchManager = branchManager;
        }
        [HttpGet]
        public IHttpActionResult GetProjectBranches(int id)
        {
            List<Branch> branches = _BranchManager.branches(id);
            if(branches == null)
            {
                return NotFound();
            }
            return Ok(branches);
        }

        [HttpGet]
        public IHttpActionResult GetBranch(int id)
        {
            Branch branch = _BranchManager.GetBranch(id);
            if (branch == null)
            {
                return NotFound();
            }
            return Ok(branch);
        }

        [HttpPost]
        public IHttpActionResult InsertBranch(Branch branch)
        {
            bool status = _BranchManager.InsertBranch(branch);
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public IHttpActionResult UpdateBranch(Branch branch)
        {
            bool status = _BranchManager.EditBranch(branch);
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public IHttpActionResult DeleteBranch(int id)
        {
            bool status = _BranchManager.DeleteBranch(id);
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }
    }
}
