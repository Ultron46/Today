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
    public class PackageReleaseDataRepository : IPackageReleaseDataRepository
    {
        DevOpsEntities DbContext;
        public PackageReleaseDataRepository()
        {
            DbContext = new DevOpsEntities();
        }

        public List<PackageRelease> GetAllPackages()
        {
            return DbContext.PackageReleases.ToList();

        }

        public PackageRelease GetPackage(int id)
        {
            return DbContext.PackageReleases.Where(p => p.PackageId == id).FirstOrDefault();
        }

        public bool InsertPackage(PackageRelease packageRelease)
        {
            bool status = false;
            DbContext.PackageReleases.Add(packageRelease);
            if (DbContext.SaveChanges() > 0)
            {
                status = true;
            }
            return status;

        }

        public bool UpdatePackage(PackageRelease packageRelease)
        {
            bool status = false;
            PackageRelease packageReleaseTemp = DbContext.PackageReleases.AsNoTracking().Where(p => p.PackageId == packageRelease.PackageId).FirstOrDefault();
            if (packageReleaseTemp != null)
            {
                packageReleaseTemp = packageRelease;
                DbContext.Entry(packageReleaseTemp).State = EntityState.Modified;
                if (DbContext.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool DeletePackage(int id)
        {
            bool status = false;
            PackageRelease packageRelease = DbContext.PackageReleases.Where(p => p.PackageId == id).FirstOrDefault();
            if (packageRelease != null)
            {
                DbContext.PackageReleases.Remove(packageRelease);
                if (DbContext.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public List<BuildProject> GetBuildVersion(int id)
        {
            List<BuildProject> buildProjects = DbContext.BuildProjects.Include(x => x.Project).Where(x => x.Project.OrganisationId == id).ToList();
            return buildProjects;

        }

        public int TotalBuilds(int id)
        {
            int total = 0;
            if (id == 0)
            {
                total = DbContext.ReleaseProjects.Count();
            }
            else
            {
                total = DbContext.ReleaseProjects.Where(x => x.ServerBuild.BuildProject.Project.OrganisationId == id).Count();
            }
            return total;
        }
    }
}
