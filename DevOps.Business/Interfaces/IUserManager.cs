using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Interfaces
{
    public interface IUserManager
    {
        List<User> GetAllUsers();
        User GetUser(int userId);
        User GetAuthUser(string email, string password);
        bool InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        int TotalUsers(int id);
        List<User> GetAllUsersOfOrganization(int id);
        List<User> GetRecentUsers(int id);
        User ForgotPassword(string Email);
        bool CheckEmail(string Email);
        bool UpdatePassword(string Email, string Password);
        bool ChangePassword(string Email, string CurrentPassword, string Password);
    }
}
