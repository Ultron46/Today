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
    public class PackageReleaseManager : IPackageManager
    {
        private IPackageReleaseDataRepository _packageReleaseDataRepository;
        public PackageReleaseManager() { }
        public PackageReleaseManager(IPackageReleaseDataRepository packageReleaseDataRepository)
        {
            _packageReleaseDataRepository = packageReleaseDataRepository;
        }

        public bool DeletePackage(int id)
        {
            return _packageReleaseDataRepository.DeletePackage(id);
        }

        public List<PackageRelease> GetAllPackages()
        {
            return _packageReleaseDataRepository.GetAllPackages();
        }

        public List<BuildProject> GetBuildVersion(int id)
        {

            return _packageReleaseDataRepository.GetBuildVersion(id);
        }

        public PackageRelease GetPackage(int id)
        {
            PackageRelease packageRelease = _packageReleaseDataRepository.GetPackage(id);
            return packageRelease;

        }

        public bool InsertPackage(PackageRelease packageRelease)
        {
            return _packageReleaseDataRepository.InsertPackage(packageRelease);
        }

        public int TotalBuilds(int id)
        {
            int total = _packageReleaseDataRepository.TotalBuilds(id);
            return total;
        }

        public bool UpdatePackage(PackageRelease packageRelease)
        {
            return _packageReleaseDataRepository.UpdatePackage(packageRelease);
        }
    }
}
