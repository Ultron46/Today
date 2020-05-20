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
        public List<SupportTicket> GetAllTicketUnfixed()
        {
            return _supportTicketDataRepository.GetAllTicketUnfixed();
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

        public int TotalSupportTickets(int id)
        {
            int total = _supportTicketDataRepository.TotalSupportTickets(id);
            return total;
        }

        public int TotalUserSupportTickets(int id)
        {
            int total = _supportTicketDataRepository.TotalUserSupportTickets(id);
            return total;
        }

        public List<SupportTicket> UserSupportTickets(int id)
        {
            List<SupportTicket> tickets = _supportTicketDataRepository.UserSupportTickets(id);
            return tickets;
        }
    }
}
