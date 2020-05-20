using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.Interfaces
{
    public interface ISupportTicketDataRepository
    {
        List<SupportTicket> GetAllTicket();
        bool InsertTicket(SupportTicket supportTicket);
        SupportTicket GetSolution(int TicketId);
        bool GetTicket(int id, int tid);
        //bool DeleteTicket(int id);
        List<SupportTicket> GetAllTicketUnfixed();
        int TotalSupportTickets(int id);
        int TotalUserSupportTickets(int id);
        List<SupportTicket> UserSupportTickets(int id);
    }
}
