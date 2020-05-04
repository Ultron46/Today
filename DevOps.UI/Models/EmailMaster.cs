using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class EmailMaster
    {
        public int EmailMasterId { get; set; }
        public string EmailId { get; set; }
        public Nullable<int> OrganisationId { get; set; }
    }
}