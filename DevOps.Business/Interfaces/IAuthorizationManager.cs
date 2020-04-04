using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Interfaces
{
    public interface IAuthorizationManager
    {
        UserToken GetToken(int id);
        bool InsertUserToken(UserToken userToken);
        bool DeleteUserToken(int id);
    }
}
