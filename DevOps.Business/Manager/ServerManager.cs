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
    public class ServerManager : IServerManager
    {
        private IServerConfigDataRepository _serverConfigDataRepository;
        private IServerCredentialDataRepository _serverCredentialDataRepository;
        public ServerManager(IServerConfigDataRepository serverConfigDataRepository, IServerCredentialDataRepository serverCredentialDataRepository)
        {
            _serverConfigDataRepository = serverConfigDataRepository;
            _serverCredentialDataRepository = serverCredentialDataRepository;
        }
        public bool DeleteServerConfig(int id)
        {
            bool status = _serverConfigDataRepository.DeleteServerConfig(id);
            return status;
        }

        public bool DeleteServerCredential(int id)
        {
            bool status = _serverCredentialDataRepository.DeleteServerCredential(id);
            return status;
        }

        public ServerConfig GetServerConfig(int id)
        {
            ServerConfig serverConfig = _serverConfigDataRepository.GetServerConfig(id);
            return serverConfig;
        }

        public List<ServerConfig> GetServerConfigs(int id)
        {
            List<ServerConfig> serverConfigs = _serverConfigDataRepository.GetServerConfigs(id);
            return serverConfigs;
        }

        public ServerCredential GetServerCredential(int id)
        {
            ServerCredential serverCredential = _serverCredentialDataRepository.GetServerCredential(id);
            return serverCredential;
        }

        public List<ServerCredential> GetServerCredentials(int ServerId)
        {
            List<ServerCredential> serverCredentials = _serverCredentialDataRepository.GetServerCredentials(ServerId);
            return serverCredentials;
        }

        public bool InsertServerConfig(ServerConfig serverConfig)
        {
            bool status = _serverConfigDataRepository.InsertServerConfig(serverConfig);
            return status;
        }

        public bool InsertServerCredential(ServerCredential serverCredential)
        {
            bool status = _serverCredentialDataRepository.InsertServerCredential(serverCredential);
            return status;
        }

        public bool UpdateServerConfig(ServerConfig serverConfig)
        {
            bool status = _serverConfigDataRepository.UpdateServerConfig(serverConfig);
            return status;
        }

        public bool UpdateServerCredential(ServerCredential serverCredential)
        {
            bool status = _serverCredentialDataRepository.UpdateServerCredential(serverCredential);
            return status;
        }
        public bool InsertEmail(EmailMaster emailMaster)
        {
            bool status = _serverCredentialDataRepository.InsertEmail(emailMaster);
            return status;
        }

        public bool InsertServerBuild(int BuildId, int ServerId, int UserId)
        {
            bool status = _serverConfigDataRepository.InsertServerBuild(BuildId, ServerId, UserId);
            return status;
        }

        public List<ServerBuild> serverBuilds(int id)
        {
            List<ServerBuild> builds = _serverConfigDataRepository.serverBuilds(id);
            return builds;
        }

        public bool DeleteAllServerCredential(int id)
        {
            bool status = _serverCredentialDataRepository.DeleteAllServerCredentials(id);
            return status;
        }

        public bool UpdateServerBuildStatus(int id)
        {
            bool status = _serverConfigDataRepository.UpdateServerBuildStatus(id);
            return status;
        }

        public ServerBuild QueuedBuild()
        {
            ServerBuild build = _serverConfigDataRepository.QueuedBuild();
            return build;
        }

        public bool UpdateServerBuild(ServerBuild build)
        {
            bool status = _serverConfigDataRepository.UpdateServerBuild(build);
            return status;
        }

        public ServerBuild GetServerBuild(int id)
        {
            ServerBuild build = _serverConfigDataRepository.GetServerBuild(id);
            return build;
        }

        public int TotalBuilds(int id)
        {
            int total = _serverConfigDataRepository.TotalBuilds(id);
            return total;
        }

        public int TotalServers(int id)
        {
            int total = _serverConfigDataRepository.TotalServers(id);
            return total;
        }

        public List<ServerBuild> GetServerBuilds(int pid, int bid, int sid)
        {
            List<ServerBuild> serverBuilds = _serverConfigDataRepository.GetServerBuilds(pid, bid, sid);
            return serverBuilds;

        }
    }
}
