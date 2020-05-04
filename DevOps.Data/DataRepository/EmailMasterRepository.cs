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
    public class EmailMasterRepository : IEmailMasterRepository
    {
        private DevOpsEntities db;
        public EmailMasterRepository()
        {
            db = new DevOpsEntities();
        }
        public List<EmailMaster> GetEmails(int organizationId)
        {
            List<EmailMaster> emails = new List<EmailMaster>();
            if (organizationId == 0)
            {
                emails = db.EmailMasters.Include(x => x.Organisation).ToList();
            }
            else
            {
                emails = db.EmailMasters.Where(x => x.OrganisationId == organizationId).Include(x => x.Organisation).ToList();
            }
            return emails;
        }
        public EmailMaster GetEmailMaster(int id)
        {
            EmailMaster email = db.EmailMasters.Find(id);
            return email;
        }
        public bool InsertEmailMaster(EmailMaster email)
        {
            bool status = false;
            db.EmailMasters.Add(email);
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
        public bool UpdateEmailMaster(EmailMaster email)
        {
            bool status = false;
            EmailMaster temp = db.EmailMasters.Where(x => x.EmailMasterId == email.EmailMasterId).AsNoTracking().FirstOrDefault();
            db.Entry(email).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
        public bool DeleteEmailMaster(int id)
        {
            bool status = false;
            EmailMaster email = db.EmailMasters.Find(id);
            db.EmailMasters.Remove(email);
            if (db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
    }
}
