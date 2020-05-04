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

        [HttpGet]
        public IHttpActionResult GetOrganizationProject(int id)
        {
            List<Project> project = _projectManager.GetOrganizationProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpGet]
        public IHttpActionResult GetProjectBuilds(int id)
        {
            List<BuildProject> builds = _projectManager.ProjectBuilds(id);
            if(builds == null)
            {
                return NotFound();
            }
            return Ok(builds);
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

        [HttpGet]
        public IHttpActionResult ProjectBuild(string sourceURL)
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult InsertBuildProject(string sourceURL, int projectId, int userId)
        {
            bool status = _projectManager.BuildProject(sourceURL, projectId, userId);
            if(status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpGet]
        public IHttpActionResult Builds(int id)
        {
            List<BuildProject> builds = _projectManager.builds(id);
            if(builds == null)
            {
                return NotFound();
            }
            return Ok(builds);
        }

        [HttpGet]
        public IHttpActionResult GetProjectBuild(int id)
        {
            BuildProject project = _projectManager.ProjectBuild(id);
            if(project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }
    }
}
