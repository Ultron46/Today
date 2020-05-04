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
    public class EmailMasterManager : IEmailMasterManager
    {
        private IEmailMasterRepository _emailMasterRepository;
        public EmailMasterManager(IEmailMasterRepository emailMasterRepository)
        {
            _emailMasterRepository = emailMasterRepository;
        }

        public bool DeleteEmailMaster(int id)
        {
            bool status = _emailMasterRepository.DeleteEmailMaster(id);
            return status;
        }

        public EmailMaster GetEmailMaster(int id)
        {
            EmailMaster email = _emailMasterRepository.GetEmailMaster(id);
            return email;
        }

        public List<EmailMaster> GetEmails(int organizationId)
        {
            List<EmailMaster> emails = _emailMasterRepository.GetEmails(organizationId);
            return emails;
        }

        public bool InsertEmailMaster(EmailMaster email)
        {
            bool status = _emailMasterRepository.InsertEmailMaster(email);
            return status;
        }

        public bool UpdateEmailMaster(EmailMaster email)
        {
            bool status = _emailMasterRepository.UpdateEmailMaster(email);
            return status;
        }
    }
}
