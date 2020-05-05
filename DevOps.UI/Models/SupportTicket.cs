using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class SupportTicket
    {
        public int TicketId { get; set; }
        public Nullable<int> GeneratedBy { get; set; }
        public Nullable<int> FixedBy { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public System.DateTime GeneratedDate { get; set; }
        public Nullable<System.DateTime> FixedDate { get; set; }

        //public virtual User User { get; set; }
        public User User1 { get; set; }
    }
}