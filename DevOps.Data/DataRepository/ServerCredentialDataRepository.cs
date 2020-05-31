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
        public List<ServerCredential> GetServerCredentials(int ServerId)
        {
            return db.ServerCredentials.Where(x => x.ServerId == ServerId).ToList();
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
            ServerCredential serverCredential1 = db.ServerCredentials.Where(x => x.ServerCredentialsId == serverCredential.ServerCredentialsId).AsNoTracking().FirstOrDefault();
            serverCredential.ServerId = serverCredential1.ServerId;
            db.Entry(serverCredential).State = EntityState.Modified;
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
        public bool InsertEmail(EmailMaster emailMaster)
        {
            bool status = false;
            db.EmailMasters.Add(emailMaster);
            if (db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool DeleteAllServerCredentials(int id)
        {
            bool status = false;
            List<ServerCredential> serverCredentials = db.ServerCredentials.Where(x => x.ServerId == id).ToList();
            if (serverCredentials.Count != 0)
            {
                db.ServerCredentials.RemoveRange(serverCredentials);
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        status = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                status = true;
            }
            return status;
        }
    }
}
