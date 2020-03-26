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
    public class ServerCredentialDataRepository : IServerCredentialDataRepository
    {
        private DevOpsEntities db;

        public ServerCredentialDataRepository()
        {
            db = new DevOpsEntities();
        }
        public List<ServerCredential> GetServerCredentials()
        {
            return db.ServerCredentials.ToList();
        }

        public ServerCredential GetServerCredential(int id)
        {
            return db.ServerCredentials.Find(id);
        }

        public bool InsertServerCredential(ServerCredential serverCredential)
        {
            bool status = false;
            db.ServerCredentials.Add(serverCredential);
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool UpdateServerCredential(ServerCredential serverCredential)
        {
            bool status = false;
            ServerCredential serverCredential1 = db.ServerCredentials.Find(serverCredential.ServerCredentialsId);
            serverCredential1 = serverCredential;
            db.Entry(serverCredential1).State = EntityState.Modified;
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool DeleteServerCredential(int id)
        {
            bool status = false;
            ServerCredential serverCredential = db.ServerCredentials.Find(id);
            db.ServerCredentials.Remove(serverCredential);
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
    }
}
