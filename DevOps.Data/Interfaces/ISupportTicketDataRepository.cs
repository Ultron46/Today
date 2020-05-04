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
        bool UpdateTicket(SupportTicket supportTicket);
        //bool DeleteTicket(int id);




    }
}
