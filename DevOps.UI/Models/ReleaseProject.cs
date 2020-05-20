using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class ReleaseProject
    {
        public int ReleaseProjectId { get; set; }

        public Nullable<int> BuildId { get; set; }

        public Nullable<int> ReleaseBy { get; set; }

        public Nullable<int> ServerId { get; set; }

        public Nullable<System.DateTime> ReleaseDate { get; set; }

        public Nullable<int> PackageId { get; set; }

        public string Status { get; set; }
    }
}