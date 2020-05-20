using DevOps.Business.Interfaces;
using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Manager
{
    public class BranchManager : IBranchManager
    {
        private IBranchDataRepository _branchDataRepository;
        public BranchManager(IBranchDataRepository branchDataRepository)
        {
            _branchDataRepository = branchDataRepository;
        }
        public List<Branch> branches(int id)
        {
            List<Branch> branches = _branchDataRepository.branches(id);
            return branches;
        }

        public bool DeleteBranch(int id)
        {
            bool status = _branchDataRepository.DeleteBranch(id);
            return status;
        }

        public bool EditBranch(Branch branch)
        {
            bool status = _branchDataRepository.EditBranch(branch);
            return status;
        }

        public Branch GetBranch(int id)
        {
            Branch branch = _branchDataRepository.GetBranch(id);
            return branch;
        }

        public bool InsertBranch(Branch branch)
        {
            bool status = _branchDataRepository.InsertBranch(branch);
            return status;
        }
    }
}
