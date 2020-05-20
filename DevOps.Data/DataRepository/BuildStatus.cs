using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.DataRepository
{
    public class BuildStatus : IBuildStatus
    {
        DevOpsEntities db;
        public BuildStatus()
        {
            db = new DevOpsEntities();
        }
        public int TotalFailedBuilds(int id)
        {
            int total = 0;
            int builds = 0;
            int sbuilds = 0;
            int release = 0;
            if(id == 0)
            {
                builds = db.BuildProjects.Where(x => x.Status.Equals("failure")).Count();
                sbuilds = db.ServerBuilds.Where(x => x.Status.Equals("failure")).Count();
                release = db.ReleaseProjects.Where(x => x.Status.Equals("failure")).Count();
            }
            else
            {
                builds = db.BuildProjects.Where(x => x.Status.Equals("failure") && x.Project.OrganisationId == id).Count();
                sbuilds = db.ServerBuilds.Where(x => x.Status.Equals("failure") && x.BuildProject.Project.OrganisationId == id).Count();
                release = db.ReleaseProjects.Where(x => x.Status.Equals("failure") && x.BuildProject.Project.OrganisationId == id).Count();
            }
            total = builds + sbuilds + release;
            return total;
        }

        public int TotalQueuedBuilds(int id)
        {
            int total = 0;
            int builds = 0;
            int sbuilds = 0;
            int release = 0;
            if (id == 0)
            {
                builds = db.BuildProjects.Where(x => x.Status.Equals("queued")).Count();
                sbuilds = db.ServerBuilds.Where(x => x.Status.Equals("queued")).Count();
                release = db.ReleaseProjects.Where(x => x.Status.Equals("queued")).Count();
            }
            else
            {
                builds = db.BuildProjects.Where(x => x.Status.Equals("queued") && x.Project.OrganisationId == id).Count();
                sbuilds = db.ServerBuilds.Where(x => x.Status.Equals("queued") && x.BuildProject.Project.OrganisationId == id).Count();
                release = db.ReleaseProjects.Where(x => x.Status.Equals("queued") && x.BuildProject.Project.OrganisationId == id).Count();
            }
            total = builds + sbuilds + release;
            return total;
        }

        public int TotalSuccessfulBuilds(int id)
        {
            int total = 0;
            int builds = 0;
            int sbuilds = 0;
            int release = 0;
            if (id == 0)
            {
                builds = db.BuildProjects.Where(x => x.Status.Equals("success")).Count();
                sbuilds = db.ServerBuilds.Where(x => x.Status.Equals("success")).Count();
                release = db.ReleaseProjects.Where(x => x.Status.Equals("success")).Count();
            }
            else
            {
                builds = db.BuildProjects.Where(x => x.Status.Equals("success") && x.Project.OrganisationId == id).Count();
                sbuilds = db.ServerBuilds.Where(x => x.Status.Equals("success") && x.BuildProject.Project.OrganisationId == id).Count();
                release = db.ReleaseProjects.Where(x => x.Status.Equals("success") && x.BuildProject.Project.OrganisationId == id).Count();
            }
            total = builds + sbuilds + release;
            return total;
        }

        public int UserFailedBuilds(int id)
        {
            int total = 0;
            total = db.BuildProjects.Where(x => x.BuildBy == id && x.Status.Equals("failure")).Count();
            return total;
        }

        public int UserQueuedBuilds(int id)
        {
            int total = 0;
            total = db.BuildProjects.Where(x => x.BuildBy == id && x.Status.Equals("queued")).Count();
            return total;
        }

        public int UserSuccessfulBuilds(int id)
        {
            int total = 0;
            total = db.BuildProjects.Where(x => x.BuildBy == id && x.Status.Equals("success")).Count();
            return total;
        }
    }
}
