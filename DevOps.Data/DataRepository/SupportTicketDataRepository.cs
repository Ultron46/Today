using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DevOps.Data.DataRepository
{
    public class SupportTicketDataRepository : ISupportTicketDataRepository
    {
        DevOpsEntities DbContext;
        public SupportTicketDataRepository()
        {
            DbContext = new DevOpsEntities();
        }

        //public List<Organisation> GetAllTicket()
        //{
        //  return DbContext.Organisations.ToList();
        //}

        public List<SupportTicket> GetAllTicket()
        {
            List<SupportTicket> supportTickets = DbContext.SupportTickets.Include(x => x.User).Include(x => x.User1).ToList();
            return supportTickets;
            //return DbContext.SupportTickets.AsNoTracking().ToList();
        }

        public bool InsertTicket(SupportTicket supportTicket)
        {
            bool status = false;
            DbContext.SupportTickets.Add(supportTicket);
            if (DbContext.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
        public SupportTicket GetSolution(int TicketId)
        {
            SupportTicket ticket = new SupportTicket();
            try
            {
                ticket = DbContext.SupportTickets.Where(x => x.TicketId == TicketId).Include(x => x.User1).FirstOrDefault();
                return ticket;
            }
            catch (Exception e)
            {
                return ticket;
            }
            //return DbContext.SupportTickets.Where(p => p.TicketId == TicketId).FirstOrDefault();
        }
        public bool GetTicket(int id, int tid)
        {
            SupportTicket supportTicket = DbContext.SupportTickets.Where(p => p.TicketId == tid).FirstOrDefault();
            supportTicket.Status = "fixed";
            supportTicket.FixedDate = DateTime.Now;
            supportTicket.FixedBy = id;
            //supportTicket.FixedBy = Session["user"];
            DbContext.Entry(supportTicket).State = EntityState.Modified;
            if (DbContext.SaveChanges() > 0)
                return true;
            return false;
        }

    }
}
