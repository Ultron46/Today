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
        public IHttpActionResult GetProjects(int id)
        {
            List<Project> projects = _projectManager.GetProjects(id);
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

        [HttpPost]
        public IHttpActionResult InsertBuildProject(BuildProject build)
        {
            bool status = _projectManager.BuildProject(build);
            if(status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public IHttpActionResult UpdateBuildProject(BuildProject build)
        {
            bool status = _projectManager.UpdateBuildProject(build);
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
        public IHttpActionResult BranchBuilds(int id)
        {
            List<BuildProject> builds = _projectManager.BranchBuilds(id);
            if (builds == null)
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

        [HttpGet]
        public IHttpActionResult UpdateProjectBuildStatus(int id)
        {
            bool status = _projectManager.UpdateProjectBuildStatus(id);
            return Ok(status);
        }

        [HttpGet]
        public IHttpActionResult GetQueuedBuild()
        {
            BuildProject build = _projectManager.QueuedProjectBuild();
            return Ok(build);
        }

        [HttpPost]
        public IHttpActionResult ReleaseProject(ReleaseProject releaseProject)
        {
            bool status = _projectManager.ReleaseProject(releaseProject);
            if (status)
                return Ok(status);
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetReleaseProject(int id)
        {
            List<ReleaseProject> releaseProjects = _projectManager.GetReleaseProject(id);
            if (releaseProjects == null)
            {
                return NotFound();
            }
            return Ok(releaseProjects);
        }

        [HttpPost]
        public bool RebuildProject(int userid, int id)
        {
            return _projectManager.RebuildProject(userid, id);
        }

        public IHttpActionResult GetTotalProjects(int id)
        {
            int total = _projectManager.TotalProjects(id);
            return Ok(total);
        }

        public IHttpActionResult GetTotalBuilds(int id)
        {
            int total = _projectManager.TotalBuilds(id);
            return Ok(total);
        }

        public IHttpActionResult GetRecentProjects(int id)
        {
            List<Project> projects = _projectManager.GetRecentProjects(id);
            return Ok(projects);
        }

        public IHttpActionResult GetUserBuilds(int id)
        {
            List<BuildProject> builds = _projectManager.UserBuilds(id);
            return Ok(builds);
        }

        public IHttpActionResult GetTotalUserBuilds(int id)
        {
            int total = _projectManager.TotalUserBuilds(id);
            return Ok(total);
        }

        [HttpGet]
        public IHttpActionResult GetQueuedBuildProject()
        {
            ReleaseProject releaseProject = _projectManager.GetQueuedBuildProject();
            return Ok(releaseProject);
        }
        [HttpGet]
        public IHttpActionResult UpdateQueuedBuildProjectStatus(int id)
        {
            bool status = _projectManager.UpdateQueuedBuildProjectStatus(id);
            return Ok(status);
        }
    }
}
