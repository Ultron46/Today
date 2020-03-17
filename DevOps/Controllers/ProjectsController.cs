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
    public class ProjectsController : ApiController
    {
        private IProjectManager _projectManager;

        public ProjectsController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [HttpGet]
        public IHttpActionResult GetProjects()
        {
            List<Project> projects = _projectManager.GetProjects();
            if(projects == null)
            {
                return NotFound();
            }
            return Ok(projects);
        }

        [HttpGet]
        public IHttpActionResult GetProject(int id)
        {
            Project project = _projectManager.GetProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public IHttpActionResult InsertProject(Project project)
        {
            bool status = _projectManager.InsertProject(project);
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }
        [HttpPost]
        public bool UpdateProject(Project project)
        {
            return _projectManager.UpdateProject(project);
        }

        [HttpPost]
        public IHttpActionResult DeleteProject(int id)
        {
            bool status = _projectManager.DeleteProject(id);
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }
    }
}
