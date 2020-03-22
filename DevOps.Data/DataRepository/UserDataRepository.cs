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
    public class UserDataRepository : IUserDataRepository
    {

        DevOpsEntities DbContext;
        public UserDataRepository()
        {
            DbContext = new DevOpsEntities();
        }

        public List<User> GetAllUsers()
        {
            return DbContext.Users.Include(x => x.Organisation).ToList();
        }
        public User GetUser(int userId)
        {
            return DbContext.Users.Where(p => p.UserId == userId).FirstOrDefault();
        }
        public bool InsertUser(User user)
        {
            bool status = false;
            DbContext.Users.Add(user);
            if(DbContext.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
        public bool UpdateUser(User user)
        {
            bool status = false;
            User u = DbContext.Users.AsNoTracking().Where( x => x.UserId == user.UserId).FirstOrDefault();
            if(user.DateOfBirth == null)
            {
                user.DateOfBirth = u.DateOfBirth;
            }
            if (user.OrganisationId == null)
            {
                user.OrganisationId = u.OrganisationId;
            }
            if (user.RoleId == null)
            {
                user.RoleId = u.RoleId;
            }
            DbContext.Entry(user).State = EntityState.Modified;
            if(DbContext.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
        public bool DeleteUser(int id)
        {
            bool status = false;
            User user = DbContext.Users.Where(p => p.UserId == id).FirstOrDefault();
            if (user != null)
            {
                DbContext.Users.Remove(user);
                if(DbContext.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            return status;
        }
    }
}
