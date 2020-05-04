using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DevOps.Data.DataRepository
{
    public class ProjectDataRepository : IProjectDataRepository
    {
        DevOpsEntities db;
        public ProjectDataRepository()
        {
            db = new DevOpsEntities();
        }
        public Project GetProject(int id)
        {
            Project p = db.Projects.Find(id);
            return p;
        }

        public List<Project> GetProjects()
        {
            List<Project> projects = db.Projects.Include(x => x.Organisation).Include(x => x.User).Include(x => x.User1).ToList();
            return projects;
        }

        public bool InsertProject(Project project)
        {
            bool status = false;
            db.Projects.Add(project);
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool UpdateProject(Project project)
        {
            bool status = false;
            Project temp = db.Projects.AsNoTracking().Where(x => x.ProjectId == project.ProjectId).FirstOrDefault();
            project.CreatedBy = temp.CreatedBy;
            project.CreatedDate = temp.CreatedDate;
            project.OrganisationId = temp.OrganisationId;
            project.LastModifiedDate = DateTime.Now;
            db.Entry(project).State = EntityState.Modified;
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool DeleteProject(int id)
        {
            bool status = false;
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool BuildProject(string sourceURL, int projectId, int userId)
        {
            bool status = false;
            BuildProject buildProject = new BuildProject();
            try
            {
                BuildProject build = db.BuildProjects.Where(x => x.ProjectId == projectId).OrderByDescending(x => x.BuildDate).FirstOrDefault();
                buildProject.BuildBy = userId;
                buildProject.BuildDate = DateTime.Now;
                buildProject.ProjectId = projectId;
                buildProject.Build_Version = build == null ? 1001 : build.Build_Version + 1;
                buildProject.Mejor_Version = build == null ? 1 : build.Mejor_Version;
                buildProject.Minor_Version = build == null ? 1 : build.Minor_Version;
                buildProject.DownloadURL = sourceURL;
                buildProject.Status = "queued";
                db.BuildProjects.Add(buildProject);
                if (db.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
            return status;
        }

        public List<BuildProject> builds(int id)
        {
            List<BuildProject> builds = new List<BuildProject>();
            try
            {
                if (id == 0)
                {
                    builds = db.BuildProjects.Include(x => x.User).Include(x => x.Project).ToList();
                }
                else
                {
                    builds = db.BuildProjects.Where(x => x.Project.OrganisationId == id).Include(x => x.User).Include(x => x.Project).ToList();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return builds;
        }

        public List<Project> GetOrganizationProject(int id)
        {
            List<Project> projects = db.Projects.Where(x => x.OrganisationId == id).ToList();
            return projects;
        }

        public List<BuildProject> ProjectBuilds(int id)
        {
            List<BuildProject> builds = db.BuildProjects.Where(x => x.Project.OrganisationId == id).Include(x => x.User).Include(x => x.Project).ToList();
            return builds;
        }

        public BuildProject ProjectBuild(int id)
        {
            BuildProject project = db.BuildProjects.Where(x => x.BuildId == id).Include(x => x.Project).SingleOrDefault();
            return project;
        }
    }
}
