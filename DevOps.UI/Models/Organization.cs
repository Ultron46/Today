using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class Organization
    {
        public int OrganisationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string Type { get; set; }
    }
}