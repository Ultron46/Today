﻿using DevOps.Data.Interfaces;
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
            List<SupportTicket> supportTickets = DbContext.SupportTickets.Where(x => x.Status == "Fixed").Include(x => x.User).Include(x => x.User1).ToList();
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

        public List<SupportTicket> GetAllTicketUnfixed()
        {
            List<SupportTicket> supportTickets = DbContext.SupportTickets.Where(x => x.Status == "Paynding").Include(x => x.User).Include(x => x.User1).ToList();
            return supportTickets;
        }

        public int TotalSupportTickets(int id)
        {
            int total = 0;
            if(id == 0)
            {
                total = DbContext.SupportTickets.Count();
            }
            else
            {
                total = DbContext.SupportTickets.Where(x => x.User1.OrganisationId == id).Count();
            }
            return total;
        }

        public int TotalUserSupportTickets(int id)
        {
            int total = 0;
            total = DbContext.SupportTickets.Where(x => x.GeneratedBy == id).Count();
            return total;
        }

        public List<SupportTicket> UserSupportTickets(int id)
        {
            List<SupportTicket> tickets = DbContext.SupportTickets.Where(x => x.GeneratedBy == id).OrderByDescending(x => x.GeneratedDate).Take(5).ToList();
            return tickets;
        }

        public int GetTotalFixSupportTickets(int id)
        {
            int total = 0;
            if (id == 0)
            {
                total = DbContext.SupportTickets.Where(x => x.Status == "Fixed").Count();
            }
            else
            {
                total = DbContext.SupportTickets.Where(x => x.Status == "Fixed" && x.User1.OrganisationId == id).Count();
            }
            return total;
        }

        public int GetTotalNotFixedSupportTicket(int id)
        {
            int total = 0;
            if (id == 0)
            {
                total = DbContext.SupportTickets.Where(x => x.Status == "Paynding").Count();
            }
            else
            {
                total = DbContext.SupportTickets.Where(x => x.Status == "Paynding" && x.User1.OrganisationId == id).Count();
            }
            return total;
        }

        public int GetTotalFixSupportTicketsOfUsers(int id)
        {
            int total = 0;
            total = DbContext.SupportTickets.Where(x => x.GeneratedBy == id && x.Status == "Fixed").Count();
            return total;
        }

        public int GetTotalNotFixedSupportTicketOfUsers(int id)
        {
            int total = 0;
            total = DbContext.SupportTickets.Where(x => x.GeneratedBy == id && x.Status == "Paynding").Count();
            return total;

        }
    }
}
