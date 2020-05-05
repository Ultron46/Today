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
    public class SupportTicketManager : ISupportTicketManager
    {

        private ISupportTicketDataRepository _supportTicketDataRepository;

        public SupportTicketManager() { }

        public SupportTicketManager(ISupportTicketDataRepository supportTicketManager)
        {
            _supportTicketDataRepository = supportTicketManager;
        }




        public List<SupportTicket> GetAllTicket()
        {
            return _supportTicketDataRepository.GetAllTicket();
        }

        public bool InsertTicket(SupportTicket supportTicket)
        {
            return _supportTicketDataRepository.InsertTicket(supportTicket);
        }

        public SupportTicket GetSolution(int TicketId)
        {
            return _supportTicketDataRepository.GetSolution(TicketId);
        }
        public bool GetTicket(int id, int tid)
        {
            return _supportTicketDataRepository.GetTicket(id, tid);
        }


    }
}
