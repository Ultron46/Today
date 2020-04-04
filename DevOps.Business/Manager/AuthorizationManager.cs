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
    public class AuthorizationManager : IAuthorizationManager
    {
        private IAuthorizationDataRepository _authorizationDataRepository;
        public AuthorizationManager(IAuthorizationDataRepository authorizationDataRepository)
        {
            _authorizationDataRepository = authorizationDataRepository;
        }
        public bool DeleteUserToken(int id)
        {
            bool status = _authorizationDataRepository.DeleteUserToken(id);
            return status;
        }

        public UserToken GetToken(int id)
        {
            UserToken userToken = _authorizationDataRepository.GetToken(id);
            return userToken;
        }

        public bool InsertUserToken(UserToken userToken)
        {
            bool status = _authorizationDataRepository.InsertUserToken(userToken);
            return status;
        }
    }
}
