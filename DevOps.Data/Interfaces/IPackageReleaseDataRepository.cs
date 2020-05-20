using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.Interfaces
{
    public interface IPackageReleaseDataRepository
    {
        bool InsertPackage(PackageRelease packageRelease);
        List<PackageRelease> GetAllPackages();
        PackageRelease GetPackage(int id);
        bool UpdatePackage(PackageRelease packageRelease);
        bool DeletePackage(int id);
        List<BuildProject> GetBuildVersion(int id);
        int TotalBuilds(int id);
    }
}
