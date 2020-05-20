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

    public class PackageController : ApiController
    {
        private IPackageManager _packageManager;
        public PackageController() { }
        public PackageController(IPackageManager packageManager)
        {
            _packageManager = packageManager;
        }

        [HttpPost]
        public IHttpActionResult InsertPackage(PackageRelease packageRelease)
        {
            bool status = _packageManager.InsertPackage(packageRelease);
            if (status)
                return Ok(status);
            return NotFound();
        }

        public IHttpActionResult GetAllPackages()
        {
            var packages = _packageManager.GetAllPackages();
            if (packages == null)
            {
                return NotFound();
            }
            return Ok(packages);
        }

        [HttpGet]
        public IHttpActionResult GetPackage(int id)
        {
            PackageRelease packageRelease = _packageManager.GetPackage(id);
            if (packageRelease == null)
            {
                return NotFound();
            }
            return Ok(packageRelease);
        }

        [HttpPost]
        public bool UpdatePackage(PackageRelease packageRelease)
        {
            return _packageManager.UpdatePackage(packageRelease);
        }

        [HttpPost]
        public IHttpActionResult DeletePackage(int id)
        {
            bool status = _packageManager.DeletePackage(id);
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpGet]
        public IHttpActionResult GetBuildVersion(int id)
        {
            List<BuildProject> buildProjects = _packageManager.GetBuildVersion(id);
            if (buildProjects == null)
            {
                return NotFound();
            }
            return Ok(buildProjects);
        }

        public IHttpActionResult GetTotalBuilds(int id)
        {
            int total = _packageManager.TotalBuilds(id);
            return Ok(total);
        }

    }
}
