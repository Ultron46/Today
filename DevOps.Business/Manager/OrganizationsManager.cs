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
    public class OrganizationsManager : IOrganizationsManager
    {
        private IOrganizationsDataRepository _organizationsDataRepository;

        public OrganizationsManager() { }

        public OrganizationsManager(IOrganizationsDataRepository organizationsDataRepository)
        {
            _organizationsDataRepository = organizationsDataRepository;
        }

     

        public List<Organisation> GetAllOrganization()
        {
            return _organizationsDataRepository.GetAllOrganization();
        }

        public Organisation GetOrganization(int OrganisationId)
        {
            return _organizationsDataRepository.GetOrganization(OrganisationId);
        }

        public bool InsertOrganization(Organisation organisation)
        {
            return _organizationsDataRepository.InsertOrganization(organisation);
        }

        public bool UpdateOrganization(Organisation organisation)
        {
            return _organizationsDataRepository.UpdateOrganization(organisation);
        }

        public bool DeleteOrganization(int id)
        {
            return _organizationsDataRepository.DeleteOrganization(id);
        }

        public int TotalOrganizations()
        {
            int total = _organizationsDataRepository.TotalOrganizations();
            return total;
        }

        public List<Organisation> GetRecentOrganizations()
        {
            List<Organisation> organisations = _organizationsDataRepository.GetRecentOrganizations();
            return organisations;
        }
    }
}
