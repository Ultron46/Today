using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Interfaces
{
    public interface IProjectManager
    {
        List<Project> GetProjects();
        Project GetProject(int id);
        bool InsertProject(Project project);
        bool UpdateProject(Project project);
        bool DeleteProject(int id);
        bool BuildProject(string sourceURL, int projectId, int userId);
        List<BuildProject> builds(int id);
        List<BuildProject> ProjectBuilds(int id);
        BuildProject ProjectBuild(int id);
        List<Project> GetOrganizationProject(int id);
    }
}
