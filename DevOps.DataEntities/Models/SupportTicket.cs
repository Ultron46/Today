//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevOps.DataEntities.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupportTicket
    {
        public int TicketId { get; set; }
        public Nullable<int> GeneratedBy { get; set; }
        public Nullable<int> FixedBy { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public System.DateTime GeneratedDate { get; set; }
        public Nullable<System.DateTime> FixedDate { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
