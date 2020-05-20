using DevOps.Business.Interfaces;
using DevOps.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Manager
{
    public class BuildStatusManager : IBuildStatusManager
    {
        private IBuildStatus _buildStatus;
        public BuildStatusManager(IBuildStatus buildStatus)
        {
            _buildStatus = buildStatus;
        }
        public int TotalFailedBuilds(int id)
        {
            int total = _buildStatus.TotalFailedBuilds(id);
            return total;
        }

        public int TotalQueuedBuilds(int id)
        {
            int total = _buildStatus.TotalQueuedBuilds(id);
            return total;
        }

        public int TotalSuccessfulBuilds(int id)
        {
            int total = _buildStatus.TotalSuccessfulBuilds(id);
            return total;
        }

        public int UserFailedBuilds(int id)
        {
            int total = _buildStatus.UserFailedBuilds(id);
            return total;
        }

        public int UserQueuedBuilds(int id)
        {
            int total = _buildStatus.UserQueuedBuilds(id);
            return total;
        }

        public int UserSuccessfulBuilds(int id)
        {
            int total = _buildStatus.UserSuccessfulBuilds(id);
            return total;
        }
    }
}
