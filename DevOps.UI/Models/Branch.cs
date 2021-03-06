﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class Branch
    {
        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public Nullable<int> ProjectId { get; set; }

        public Nullable<byte> Mejor_Version { get; set; }

        public Nullable<byte> Minor_Version { get; set; }

        public Nullable<int> Build_Version { get; set; }
    }
}