using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Interfaces
{
    public interface ISupportTicketManager
    {
        List<SupportTicket> GetAllTicket();
        SupportTicket GetSolution(int TicketId);
        bool InsertTicket(SupportTicket supportTicket);
        bool GetTicket(int id, int tid);
        //bool DeleteTicket(int id);
        List<SupportTicket> GetAllTicketUnfixed();
        int TotalSupportTickets(int id);
        int TotalUserSupportTickets(int id);
        List<SupportTicket> UserSupportTickets(int id);
        //for chart
        int GetTotalFixSupportTickets(int id);
        int GetTotalNotFixedSupportTicket(int id);
        int GetTotalFixSupportTicketsOfUsers(int id);
        int GetTotalNotFixedSupportTicketOfUsers(int id);
    }
}
