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
    public class OrganizationsDataRepository : IOrganizationsDataRepository
    {
        DevOpsEntities DbContext;

        public OrganizationsDataRepository()
        {
            DbContext = new DevOpsEntities();
        }

        public List<Organisation> GetAllOrganization()
        {
            return DbContext.Organisations.ToList();
        }

        public Organisation GetOrganization(int OrganisationId)
        {
            return DbContext.Organisations.Where(p => p.OrganisationId == OrganisationId).FirstOrDefault();
        }

        public bool InsertOrganization(Organisation organisation)
        {
            bool status = false;
            DbContext.Organisations.Add(organisation);
            if(DbContext.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool UpdateOrganization(Organisation organisation)
        {
            bool status = false;
            Organisation organizationTemp = DbContext.Organisations.AsNoTracking().Where(p => p.OrganisationId == organisation.OrganisationId).FirstOrDefault();
            if (organizationTemp != null)
            {
                organizationTemp = organisation;
                DbContext.Entry(organizationTemp).State = EntityState.Modified;
                if(DbContext.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool DeleteOrganization(int id)
        {
            bool status = false;
            Organisation organisation = DbContext.Organisations.Where(p => p.OrganisationId == id).FirstOrDefault();
            if (organisation != null)
            {
                DbContext.Organisations.Remove(organisation);
                if(DbContext.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public int TotalOrganizations()
        {
            int total = 0;
            total = DbContext.Organisations.Count();
            return total;
        }

        public List<Organisation> GetRecentOrganizations()
        {
            List<Organisation> organisations = new List<Organisation>();
            organisations = DbContext.Organisations.OrderBy(x => x.Name).Take(5).ToList();
            return organisations;
        }

        public List<Organisation> NumberOfUserInEachOrganization()
        {
            return DbContext.Organisations.Include("Users").OrderBy(q => q.OrganisationId).ToList();
        }

        public List<Organisation> NumberOfProjectInEachOrganization()
        {
            return DbContext.Organisations.Include("Projects").OrderBy(q => q.OrganisationId).ToList();
        }
    }
}
