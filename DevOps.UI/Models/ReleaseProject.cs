using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class ReleaseProject
    {
        public int ReleaseProjectId { get; set; }

        public string ReleaseName { get; set; }

        public int ServerBuildId { get; set; }

        public int BranchId { get; set; }

        public int ReleaseBy { get; set; }

        public System.DateTime ReleaseDate { get; set; }

        public string Status { get; set; }

        public string DownloadURL { get; set; }
        public string ReleaseNotes { get; set; }

        public ServerBuild ServerBuild { get; set; }
    }
}