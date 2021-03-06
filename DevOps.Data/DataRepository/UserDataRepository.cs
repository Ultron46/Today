﻿using DevOps.Common;
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
            user.RegistrationDate = DateTime.Now;
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
            if (string.IsNullOrEmpty(user.Password))
            {
                user.Password = u.Password;
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

        public User GetAuthUser(string email, string password)
        {
            password = Helpers.Hash(password);
            return DbContext.Users.Include(x => x.Organisation).Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
        
        public List<User> GetAllUsersOfOrganization(int id)
        {
            List<User> users = DbContext.Users.Where(x => x.OrganisationId == id).ToList();
            

            return users;
        }

        public int TotalUsers(int id)
        {
            int total = 0;
            if(id == 0)
            {
                total = DbContext.Users.Count();
            }
            else
            {
                total = DbContext.Users.Where(x => x.OrganisationId == id).Count();
            }
            return total;
        }

        public List<User> GetRecentUsers(int id)
        {
            List<User> users = DbContext.Users.Where(x => x.OrganisationId == id).OrderBy(x => x.Name).Take(5).ToList();
            return users;
        }

        public User ForgotPassword(string Email)
        {
            return DbContext.Users.Where(x => x.Email == Email).FirstOrDefault();

        }


        public bool CheckEmail(string Email)
        {
            bool status = true;

            var result = DbContext.Users.ToList().Exists(x => x.Email.Equals(Email, StringComparison.CurrentCultureIgnoreCase));
            if (result == false)
            {
                status = false;

            }
            return status;
        }

        public bool UpdatePassword(string Email, string Password)
        {
            bool status = false;
            User user = DbContext.Users.Where(x => x.Email == Email).FirstOrDefault();
            user.Password = Helpers.Hash(Password);
            DbContext.Entry(user).State = EntityState.Modified;
            if (DbContext.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool ChangePassword(string Email, string CurrentPassword, string Password)
        {
            bool status = false;
            User user = DbContext.Users.Where(x => x.Email == Email).FirstOrDefault();
            if ( Helpers.Hash(CurrentPassword) == user.Password)
            {
                user.Password = Helpers.Hash(Password);
                DbContext.Entry(user).State = EntityState.Modified;
                if (DbContext.SaveChanges() > 0)
                {
                    status = true;
                }

            }
            return status;

        }
    }
}
