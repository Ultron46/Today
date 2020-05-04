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

        public List<ServerConfig> GetServerConfigs(int id)
        {
            List<ServerConfig> servers = new List<ServerConfig>();
            if(id == 0)
            {
                servers = db.ServerConfigs.ToList();
            }
            else
            {
                servers = db.ServerConfigs.Where(x => x.OrganisationId == id).ToList();
            }
            return servers;
        }

        public ServerConfig GetServerConfig(int id)
        {
            return db.ServerConfigs.AsNoTracking().Where(x => x.ServerId == id).Include(x => x.ServerCredentials).FirstOrDefault();
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
            ServerConfig serverConfig1 = db.ServerConfigs.AsNoTracking().Where(x => x.ServerId == serverConfig.ServerId).FirstOrDefault();
            serverConfig.OrganisationId = serverConfig1.OrganisationId;
            db.Entry(serverConfig).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
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
            if (db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public bool InsertServerBuild(int BuildId, int ServerId, int UserId)
        {
            bool status = false;
            ServerBuild serverBuild = new ServerBuild();
            try
            {
                ServerBuild build = db.ServerBuilds.Where(x => x.BuildId == BuildId).OrderByDescending(x => x.PublishDate).FirstOrDefault();
                serverBuild.BuildId = BuildId;
                serverBuild.Build_Version = build == null ? 1001 : build.Build_Version + 1;
                serverBuild.Mejor_Version = build == null ? 1 : build.Mejor_Version;
                serverBuild.Minor_Version = build == null ? 1 : build.Minor_Version;
                serverBuild.PublishedBy = UserId;
                serverBuild.ServerId = ServerId;
                serverBuild.PublishDate = DateTime.Now;
                db.ServerBuilds.Add(serverBuild);
                if(db.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return status;
        }

        public List<ServerBuild> serverBuilds(int id)
        {
            List<ServerBuild> serverBuilds = new List<ServerBuild>();
            if(id == 0)
            {
                serverBuilds = db.ServerBuilds.Include(x => x.BuildProject).Include(x => x.User).Include(x => x.BuildProject.Project).Include(x => x.ServerConfig).ToList();
            }
            else
            {
                serverBuilds = db.ServerBuilds.Where(x => x.BuildProject.Project.OrganisationId == id).Include(x => x.BuildProject).Include(x => x.User).Include(x => x.BuildProject.Project).Include(x => x.ServerConfig).ToList();
            }
            return serverBuilds;
        }
    }
}
