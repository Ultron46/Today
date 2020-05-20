﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Interfaces
{
    public interface IBuildStatusManager
    {
        int TotalQueuedBuilds(int id);
        int TotalSuccessfulBuilds(int id);
        int TotalFailedBuilds(int id);
        int UserQueuedBuilds(int id);
        int UserSuccessfulBuilds(int id);
        int UserFailedBuilds(int id);
    }
}
