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

        public List<ServerConfig> GetServerConfigs()
        {
            List<ServerConfig> serverConfigs = _serverConfigDataRepository.GetServerConfigs();
            return serverConfigs;
        }

        public ServerCredential GetServerCredential(int id)
        {
            ServerCredential serverCredential = _serverCredentialDataRepository.GetServerCredential(id);
            return serverCredential;
        }

        public List<ServerCredential> GetServerCredentials()
        {
            List<ServerCredential> serverCredentials = _serverCredentialDataRepository.GetServerCredentials();
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
    }
}
