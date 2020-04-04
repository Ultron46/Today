using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.DataRepository
{
    public class AuthorizationDataRepository : IAuthorizationDataRepository
    {
        private DevOpsEntities db;
        public AuthorizationDataRepository()
        {
            db = new DevOpsEntities();
        }
        public UserToken GetToken(int id)
        {
            return db.UserTokens.Where(x => x.UserId == id).FirstOrDefault();
        }

        public bool InsertUserToken(UserToken userToken)
        {
            bool status = false;
            db.UserTokens.Add(userToken);
            if (db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool DeleteUserToken(int id)
        {
            bool status = false;
            UserToken userToken = db.UserTokens.Where(x => x.UserId == id).FirstOrDefault();
            try
            {
                db.UserTokens.Remove(userToken);
                if (db.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            catch(Exception e)
            {

            }
            return status;
        }
    }
}
