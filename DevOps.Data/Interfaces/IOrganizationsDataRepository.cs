using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.Interfaces
{
    public interface IOrganizationsDataRepository
    {
        List<Organisation> GetAllOrganization();
        Organisation GetOrganization(int OrganisationId);
        bool InsertOrganization(Organisation organisation);
        bool UpdateOrganization(Organisation organisation);
        bool DeleteOrganization(int id);
    }
}
