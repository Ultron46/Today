using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class Projects
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string SolutionName { get; set; }
        public Nullable<int> OrganisationId { get; set; }
        public string SourceURL { get; set; }
        public string FileFormat { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Plateform { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime LastModifiedDate { get; set; }

    }
}