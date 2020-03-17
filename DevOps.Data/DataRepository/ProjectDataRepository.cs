using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
