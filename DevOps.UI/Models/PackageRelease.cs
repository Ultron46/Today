using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class PackageRelease
    {
        public int PackageId { get; set; }

        public string PackageName { get; set; }

        public string PackageVersion { get; set; }
    }
}