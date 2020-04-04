using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.Interfaces
{
    public interface IAuthorizationDataRepository
    {
        UserToken GetToken(int id);
        bool InsertUserToken(UserToken userToken);
        bool DeleteUserToken(int id);
    }
}
