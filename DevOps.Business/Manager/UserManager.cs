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
    public class UserManager : IUserManager
    {
        private IUserDataRepository _userDataRepository;

        public UserManager() { }

        public UserManager(IUserDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userDataRepository.GetAllUsers();
        }

        public User GetUser(int userId)
        {
            return _userDataRepository.GetUser(userId);
        }

        public bool InsertUser(User user)
        {
            return _userDataRepository.InsertUser(user);
        }

        public bool UpdateUser(User user)
        {
            return _userDataRepository.UpdateUser(user);
        }

        public bool DeleteUser(int id)
        {
            return _userDataRepository.DeleteUser(id);
        }

        public User GetAuthUser(string email, string password)
        {
            return _userDataRepository.GetAuthUser(email, password);
        }
        public List<User> GetAllUsersOfOrganization(int id)
        {
            return _userDataRepository.GetAllUsersOfOrganization(id);
        }

        public int TotalUsers(int id)
        {
            int total = _userDataRepository.TotalUsers(id);
            return total;
        }

        public List<User> GetRecentUsers(int id)
        {
            List<User> users = _userDataRepository.GetRecentUsers(id);
            return users;
        }

        public User ForgotPassword(String Email)
        {
            return _userDataRepository.ForgotPassword(Email);
        }

        public bool CheckEmail(string Email)
        {
            return _userDataRepository.CheckEmail(Email);
        }

        public bool UpdatePassword(string Email, string Password)
        {
            return _userDataRepository.UpdatePassword(Email, Password);
        }
    }
}
