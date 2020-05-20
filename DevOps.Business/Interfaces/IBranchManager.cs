using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Interfaces
{
    public interface IBranchManager
    {
        List<Branch> branches(int id);
        Branch GetBranch(int id);
        bool InsertBranch(Branch branch);
        bool EditBranch(Branch branch);
        bool DeleteBranch(int id);
    }
}
