using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class Emails
    {
        public int EmailMasterId { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Nullable<int> OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }


    }
}