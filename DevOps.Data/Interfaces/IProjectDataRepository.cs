using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.Interfaces
{
    public interface IProjectDataRepository
    {
        List<Project> GetProjects(int id);
        Project GetProject(int id);
        bool InsertProject(Project project);
        bool UpdateProject(Project project);
        bool DeleteProject(int id);
        bool BuildProject(BuildProject build);
        bool UpdateBuildProject(BuildProject build);
        bool UpdateProjectBuildStatus(int id);
        List<BuildProject> builds(int id);
        List<BuildProject> ProjectBuilds(int id);
        List<BuildProject> BranchBuilds(int id);
        BuildProject ProjectBuild(int id);
        BuildProject QueuedProjectBuild();
        List<Project> GetOrganizationProject(int id);
        bool ReleaseProject(ReleaseProject releaseProject);
        List<ReleaseProject> GetReleaseProject(int id);
        bool RebuildProject(int userid, int id);
        int TotalProjects(int id);
        int TotalBuilds(int id);
        int TotalUserBuilds(int id);
        List<BuildProject> UserBuilds(int id);
        List<Project> GetRecentProjects(int id);
        bool UpdateQueuedBuildProjectStatus(int id);
        ReleaseProject GetQueuedBuildProject();
    }
}
