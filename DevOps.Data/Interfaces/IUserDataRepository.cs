using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.Interfaces
{
    public interface IUserDataRepository
    {
        List<User> GetAllUsers();
        User GetUser(int userId);
        User GetAuthUser(string email, string password);
        bool InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        List<User> GetAllUsersOfOrganization(int id);
        int TotalUsers(int id);
        List<User> GetRecentUsers(int id);
        User ForgotPassword(string Email);
        bool CheckEmail(string Email);
        bool UpdatePassword(string Email, string Password);
    }
}
