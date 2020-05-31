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
                BuildProject build = db.BuildProjects.Where(x => x.BuildId == BuildId).FirstOrDefault();
                serverBuild.BuildId = BuildId;
                serverBuild.Build_Version = build.Build_Version;
                serverBuild.Mejor_Version = build.Mejor_Version;
                serverBuild.Minor_Version = build.Minor_Version;
                serverBuild.PublishedBy = UserId;
                serverBuild.ServerId = ServerId;
                serverBuild.PublishDate = DateTime.Now;
                serverBuild.Status = "queued";
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
                serverBuilds = db.ServerBuilds.Include(x => x.BuildProject).Include(x => x.User).Include(x => x.BuildProject.Project).Include(x => x.ServerConfig).Include(x => x.BuildProject.Branch).ToList();
            }
            else
            {
                serverBuilds = db.ServerBuilds.Where(x => x.BuildProject.Project.OrganisationId == id).Include(x => x.BuildProject).Include(x => x.User).Include(x => x.BuildProject.Project).Include(x => x.ServerConfig).Include(x => x.BuildProject.Branch).ToList();
            }
            serverBuilds.ForEach(x => x.BuildProject.DownloadURL = x.BuildProject.DownloadURL = "http://localhost:57996/Projects/DownloadBuildProject?fileName=" + x.BuildProject.DownloadURL.Split('\\').Last());
            return serverBuilds;
        }

        public bool UpdateServerBuildStatus(int id)
        {
            ServerBuild build = db.ServerBuilds.Find(id);
            if (build != null)
            {
                if (build.ServerBuildId % 2 == 0 || build.Status.Equals("failure"))
                {
                    build.Status = "success";
                }
                else
                {
                    build.Status = "failure";
                }
                db.Entry(build).State = EntityState.Modified;
                bool status = false;
                if (db.SaveChanges() > 0)
                {
                    status = true;
                }
                return status;
            }
            return true;
        }

        public ServerBuild QueuedBuild()
        {
            ServerBuild build = db.ServerBuilds.Where(x => x.Status.Equals("queued")).Include(x => x.BuildProject).Include(x => x.BuildProject.Project).AsNoTracking().OrderBy(x => x.PublishDate).FirstOrDefault();
            return build;
        }

        public ServerBuild GetServerBuild(int id)
        {
            ServerBuild build = db.ServerBuilds.Where(x => x.ServerBuildId == id).AsNoTracking().Include(x => x.BuildProject).Include(x => x.BuildProject.Project).FirstOrDefault();
            return build;
        }

        public bool UpdateServerBuild(ServerBuild build)
        {
            db.Entry(build).State = EntityState.Modified;
            bool status = false;
            if(db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }

        public int TotalBuilds(int id)
        {
            int total = 0;
            if(id == 0)
            {
                total = db.ServerBuilds.Count();
            }
            else
            {
                total = db.ServerBuilds.Where(x => x.BuildProject.Project.OrganisationId == id).Count();
            }
            return total;
        }

        public int TotalServers(int id)
        {
            int total = 0;
            total = db.ServerConfigs.Where(x => x.OrganisationId == id).Count();
            return total;
        }

        public List<ServerBuild> GetServerBuilds(int pid, int bid, int sid)
        {
            List<ServerBuild> serverBuilds = db.ServerBuilds.Where(x => x.BuildProject.ProjectId == pid && x.ServerConfig.ServerId == sid && x.BuildProject.BranchId == bid).Include(x => x.BuildProject).Include(x => x.User).Include(x => x.BuildProject.Project).Include(x => x.ServerConfig).Include(x => x.BuildProject.Branch).ToList();
            return serverBuilds;
        }
    }
}
