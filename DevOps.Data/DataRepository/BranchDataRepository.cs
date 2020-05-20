using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.DataRepository
{
    public class BranchDataRepository : IBranchDataRepository
    {
        private DevOpsEntities db;
        public BranchDataRepository()
        {
            db = new DevOpsEntities();
        }
        public List<Branch> branches(int id)
        {
            List<Branch> branches = new List<Branch>();
            branches = db.Branches.Where(x => x.ProjectId == id).ToList();
            return branches;
        }

        public bool DeleteBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
            bool status = false;
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool EditBranch(Branch branch)
        {
            db.Entry(branch).State = EntityState.Modified;
            bool status = false;
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public Branch GetBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            return branch;
        }

        public bool InsertBranch(Branch branch)
        {
            db.Branches.Add(branch);
            bool status = false;
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
    }
}
