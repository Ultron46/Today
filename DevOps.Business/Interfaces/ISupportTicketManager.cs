﻿using DevOps.DataEntities.Models;
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
        bool UpdateTicket(SupportTicket supportTicket);
        //bool DeleteTicket(int id);
    }
}
