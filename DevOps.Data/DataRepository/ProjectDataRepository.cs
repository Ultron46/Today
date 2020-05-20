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

        public List<Project> GetProjects(int id)
        {
            List<Project> projects = new List<Project>();
            if(id == 0)
            {
                projects = db.Projects.Include(x => x.Organisation).Include(x => x.User).Include(x => x.User1).ToList();
            }
            else
            {
                projects = db.Projects.Where(x => x.OrganisationId == id).Include(x => x.Organisation).Include(x => x.User).Include(x => x.User1).ToList();
            }
            return projects;
        }

        public bool InsertProject(Project project)
        {
            bool status = false;
            db.Projects.Add(project);
            if(db.SaveChanges() > 0)
            {
                int id = project.ProjectId;
                Branch branch = new Branch { BranchName = "master", ProjectId = id };
                db.Branches.Add(branch);
                if(db.SaveChanges() > 0)
                {
                    status = true;
                }
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

        public bool BuildProject(BuildProject project)
        {
            bool status = false;
            BuildProject buildProject = new BuildProject();
            try
            {
                BuildProject build = db.BuildProjects.Where(x => x.ProjectId == project.ProjectId).OrderByDescending(x => x.BuildDate).FirstOrDefault();
                buildProject.BuildBy = project.BuildBy;
                buildProject.BuildDate = DateTime.Now;
                buildProject.ProjectId = project.ProjectId;
                buildProject.Build_Version = build == null ? 1001 : build.Build_Version + 1;
                buildProject.Mejor_Version = build == null ? 1 : build.Mejor_Version;
                buildProject.Minor_Version = build == null ? 1 : build.Minor_Version;
                buildProject.DownloadURL = project.DownloadURL;
                buildProject.Status = project.Status;
                buildProject.BranchId = project.BranchId;
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
                    builds = db.BuildProjects.Include(x => x.User).Include(x => x.Project).Include(x => x.Branch).ToList();
                }
                else
                {
                    builds = db.BuildProjects.Where(x => x.Project.OrganisationId == id).Include(x => x.User).Include(x => x.Project).Include(x => x.Branch).ToList();
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
            List<BuildProject> builds = db.BuildProjects.Where(x => x.Project.ProjectId == id).Include(x => x.User).Include(x => x.Project).Include(x => x.Branch).ToList();
            return builds;
        }

        public BuildProject ProjectBuild(int id)
        {
            BuildProject project = db.BuildProjects.Where(x => x.BuildId == id).AsNoTracking().Include(x => x.Project).FirstOrDefault();
            return project;
        }

        public List<BuildProject> BranchBuilds(int id)
        {
            List<BuildProject> builds = db.BuildProjects.Where(x => x.BranchId == id).ToList();
            return builds;
        }

        public bool UpdateProjectBuildStatus(int id)
        {
            BuildProject build = db.BuildProjects.Find(id);
            if(build != null)
            {
                if (build.BuildId % 2 == 0 || build.Status.Equals("failure"))
                {
                    build.Status = "success";
                }
                else
                {
                    build.Status = "failure";
                }
                db.Entry(build).State = EntityState.Modified;
                bool status = false;
                if (db.SaveChanges() > 0)
                {
                    status = true;
                }
                return status;
            }
            return true;
        }

        public BuildProject QueuedProjectBuild()
        {
            BuildProject build = db.BuildProjects.Where(x => x.Status.Equals("queued")).OrderBy(x => x.BuildDate).FirstOrDefault();
            return build;
        }

        public bool UpdateBuildProject(BuildProject build)
        {
            db.Entry(build).State = EntityState.Modified;
            bool status = false;
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool ReleaseProject(ReleaseProject releaseProject)
        {
            bool status = false;
            ReleaseProject releaseProject1 = new ReleaseProject();
            try
            {
                releaseProject1.BuildId = releaseProject.BuildId;
                releaseProject1.ReleaseBy = releaseProject.ReleaseBy;
                releaseProject1.ServerId = releaseProject.ServerId;
                releaseProject1.ReleaseDate = DateTime.Now;
                releaseProject1.Status = "Queued";
                releaseProject1.PackageId = releaseProject.PackageId;
                db.ReleaseProjects.Add(releaseProject1);
                if (db.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return status;
        }

        public List<ReleaseProject> GetReleaseProject(int id)
        {
            List<ReleaseProject> releaseProjects = db.ReleaseProjects.Where(x => x.BuildProject.Project.OrganisationId == id).Include(x => x.User).Include(x => x.BuildProject).Include(x => x.BuildProject.Project).Include(x => x.ServerConfig).Include(x => x.PackageRelease).ToList();
            return releaseProjects;
        }
        public bool RebuildProject(int userid, int id)
        {
            bool status = false;
            ReleaseProject temp = db.ReleaseProjects.AsNoTracking().Where(x => x.ReleaseProjectId == id).FirstOrDefault();
            temp.ReleaseBy = userid;
            temp.ReleaseDate = DateTime.Now;
            db.Entry(temp).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public int TotalProjects(int id)
        {
            int total = 0;
            if(id == 0)
            {
                total = db.Projects.Count();
            }
            else
            {
                total = db.Projects.Where(x => x.OrganisationId == id).Count();
            }
            return total;
        }

        public int TotalBuilds(int id)
        {
            int total = 0;
            if (id == 0)
            {
                total = db.BuildProjects.Count();
            }
            else
            {
                total = db.BuildProjects.Where(x => x.Project.OrganisationId == id).Count();
            }
            return total;
        }

        public List<Project> GetRecentProjects(int id)
        {
            List<Project> projects = new List<Project>();
            if(id == 0)
            {
                projects = db.Projects.OrderByDescending(x => x.CreatedDate).Take(5).ToList();
            }
            else
            {
                projects = db.Projects.Where(x => x.OrganisationId == id).OrderByDescending(x => x.CreatedDate).Take(5).ToList();
            }
            return projects;
        }

        public int TotalUserBuilds(int id)
        {
            int total = 0;
            total = db.BuildProjects.Where(x => x.BuildBy == id).Count();
            return total;
        }

        public List<BuildProject> UserBuilds(int id)
        {
            List<BuildProject> builds = db.BuildProjects.Where(x => x.BuildBy == id).Include(x => x.Project).Include(x => x.Branch).ToList();
            return builds;
        }

        public bool UpdateQueuedBuildProjectStatus(int id)
        {
            ReleaseProject releaseProject = db.ReleaseProjects.Find(id);
            if (releaseProject != null)
            {
                if (releaseProject.ReleaseProjectId % 2 == 0 || releaseProject.Status.Equals("failure"))
                {
                    releaseProject.Status = "success";
                }
                else
                {
                    releaseProject.Status = "failure";
                }
                db.Entry(releaseProject).State = EntityState.Modified;
                bool status = false;
                if (db.SaveChanges() > 0)
                {
                    status = true;
                }
                return status;
            }
            return true;
        }

        public ReleaseProject GetQueuedBuildProject()
        {
            ReleaseProject releaseProject = db.ReleaseProjects.Where(x => x.Status.Equals("Queued")).AsNoTracking().OrderBy(x => x.ReleaseDate).FirstOrDefault();
            return releaseProject;

        }
    }
}
