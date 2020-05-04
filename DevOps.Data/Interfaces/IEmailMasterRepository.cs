using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.Interfaces
{
    public interface IEmailMasterRepository
    {
        List<EmailMaster> GetEmails(int organizationId);
        EmailMaster GetEmailMaster(int id);
        bool InsertEmailMaster(EmailMaster email);
        bool UpdateEmailMaster(EmailMaster email);
        bool DeleteEmailMaster(int id);
    }
}
