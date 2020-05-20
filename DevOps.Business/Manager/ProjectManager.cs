using DevOps.Business.Interfaces;
using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Manager
{
    public class ProjectManager : IProjectManager
    {
        private IProjectDataRepository _projectDataRepository;

        public ProjectManager(IProjectDataRepository projectDataRepository)
        {
            _projectDataRepository = projectDataRepository;
        }
        public Project GetProject(int id)
        {
            Project project = _projectDataRepository.GetProject(id);
            return project;
        }

        public List<Project> GetProjects(int id)
        {
            List<Project> projects = _projectDataRepository.GetProjects(id);
            return projects;
        }

        public bool InsertProject(Project project)
        {
            bool status = _projectDataRepository.InsertProject(project);
            return status;
        }

        public bool UpdateProject(Project project)
        {
            bool status = _projectDataRepository.UpdateProject(project);
            return status;
        }

        public bool DeleteProject(int id)
        {
            bool status = _projectDataRepository.DeleteProject(id);
            return status;
        }

        public bool BuildProject(BuildProject build)
        {
            bool status = _projectDataRepository.BuildProject(build);
            return status;
        }

        public List<BuildProject> builds(int id)
        {
            List<BuildProject> builds = _projectDataRepository.builds(id);
            return builds;
        }

        public List<Project> GetOrganizationProject(int id)
        {
            List<Project> projects = _projectDataRepository.GetOrganizationProject(id);
            return projects;
        }

        public List<BuildProject> ProjectBuilds(int id)
        {
            List<BuildProject> builds = _projectDataRepository.ProjectBuilds(id);
            return builds;
        }

        public BuildProject ProjectBuild(int id)
        {
            BuildProject project = _projectDataRepository.ProjectBuild(id);
            return project;
        }

        public List<BuildProject> BranchBuilds(int id)
        {
            List<BuildProject> builds = _projectDataRepository.BranchBuilds(id);
            return builds;
        }

        public bool UpdateProjectBuildStatus(int id)
        {
            bool status = _projectDataRepository.UpdateProjectBuildStatus(id);
            return status;
        }

        public BuildProject QueuedProjectBuild()
        {
            BuildProject build = _projectDataRepository.QueuedProjectBuild();
            return build;
        }

        public bool UpdateBuildProject(BuildProject build)
        {
            bool status = _projectDataRepository.UpdateBuildProject(build);
            return status;
        }

        public bool ReleaseProject(ReleaseProject releaseProject)
        {
            bool status = _projectDataRepository.ReleaseProject(releaseProject);
            return status;
        }

        public List<ReleaseProject> GetReleaseProject(int id)
        {
            List<ReleaseProject> releaseProjects = _projectDataRepository.GetReleaseProject(id);
            return releaseProjects;
        }

        public bool RebuildProject(int userid, int id)
        {
            bool status = _projectDataRepository.RebuildProject(userid, id);
            return status;
        }

        public int TotalProjects(int id)
        {
            int total = _projectDataRepository.TotalProjects(id);
            return total;
        }

        public int TotalBuilds(int id)
        {
            int total = _projectDataRepository.TotalBuilds(id);
            return total;
        }

        public List<Project> GetRecentProjects(int id)
        {
            List<Project> projects = _projectDataRepository.GetRecentProjects(id);
            return projects;
        }

        public int TotalUserBuilds(int id)
        {
            int total = _projectDataRepository.TotalUserBuilds(id);
            return total;
        }

        public List<BuildProject> UserBuilds(int id)
        {
            List<BuildProject> builds = _projectDataRepository.UserBuilds(id);
            return builds;
        }

        public bool UpdateQueuedBuildProjectStatus(int id)
        {
            bool status = _projectDataRepository.UpdateQueuedBuildProjectStatus(id);
            return status;
        }

        public ReleaseProject GetQueuedBuildProject()
        {
            ReleaseProject releaseProject = _projectDataRepository.GetQueuedBuildProject();
            return releaseProject;
        }
    }
}
