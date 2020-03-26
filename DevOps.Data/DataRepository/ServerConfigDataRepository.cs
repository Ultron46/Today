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
    public class ServerConfigDataRepository : IServerConfigDataRepository
    {
        private DevOpsEntities db;

        public ServerConfigDataRepository()
        {
            db = new DevOpsEntities();
        }

        public List<ServerConfig> GetServerConfigs()
        {
            return db.ServerConfigs.Include(x => x.ServerCredentials).ToList();
        }

        public ServerConfig GetServerConfig(int id)
        {
            return db.ServerConfigs.Find(id);
        }


        public bool InsertServerConfig(ServerConfig serverConfig)
        {
            bool status = false;
            db.ServerConfigs.Add(serverConfig);
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool UpdateServerConfig(ServerConfig serverConfig)
        {
            bool status = false;
            ServerConfig serverConfig1 = db.ServerConfigs.Find(serverConfig.ServerId);
            serverConfig1 = serverConfig;
            db.Entry(serverConfig1).State = EntityState.Modified;
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool DeleteServerConfig(int id)
        {
            bool status = false;
            ServerConfig serverConfig = db.ServerConfigs.Find(id);
            db.ServerConfigs.Remove(serverConfig);
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
    }
}
