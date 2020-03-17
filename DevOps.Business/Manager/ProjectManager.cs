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

        public List<Project> GetProjects()
        {
            List<Project> projects = _projectDataRepository.GetProjects();
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
    }
}
