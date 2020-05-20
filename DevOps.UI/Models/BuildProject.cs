using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class BuildProject
    {
        public int BuildId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> BuildBy { get; set; }
        public Nullable<byte> Mejor_Version { get; set; }
        public Nullable<byte> Minor_Version { get; set; }
        public Nullable<int> Build_Version { get; set; }
        public string DownloadURL { get; set; }
        public string Status { get; set; }
        public System.DateTime BuildDate { get; set; }
        public Project Project { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}