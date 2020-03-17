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
            bool status;

            try
            {

                DbContext.Organisations.Add(organisation);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool UpdateOrganization(Organisation organisation)
        {
            bool status;
            try
            {
                Organisation organizationTemp = DbContext.Organisations.AsNoTracking().Where(p => p.OrganisationId == organisation.OrganisationId).FirstOrDefault();
                if (organizationTemp != null)
                {
                    organizationTemp = organisation;
                    DbContext.Entry(organizationTemp).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteOrganization(int id)
        {
            bool status;
            try
            {
                Organisation organisation = DbContext.Organisations.Where(p => p.OrganisationId == id).FirstOrDefault();
                if (organisation != null)
                {
                    DbContext.Organisations.Remove(organisation);
                    DbContext.SaveChanges();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        
    }
}
