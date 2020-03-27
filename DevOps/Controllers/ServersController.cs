using DevOps.Business.Interfaces;
using DevOps.DataEntities.Models;
using DevOps.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DevOps.Controllers
{
    [EnableCorsAttribute("*","*","*")]
    public class ServersController : ApiController
    {
        private IServerManager _serverManager;

        public ServersController(IServerManager serverManager)
        {
            _serverManager = serverManager;
        }

        public IHttpActionResult GetServerConfigs(int Organization)
        {
            List<ServerConfig> serverConfigs = _serverManager.GetServerConfigs();
            serverConfigs = serverConfigs.Where(x => x.OrganisationId == Organization).ToList();
            if(serverConfigs == null)
            {
                return NotFound();
            }
            return Ok(serverConfigs);
        }

        public IHttpActionResult GetServerConfig(int id)
        {
            ServerConfig serverConfig = _serverManager.GetServerConfig(id);
            if (serverConfig == null)
            {
                return NotFound();
            }
            return Ok(serverConfig);
        }

        public IHttpActionResult GetServerCredentials()
        {
            List<ServerCredential> serverCredentials = _serverManager.GetServerCredentials();
            if(serverCredentials == null)
            {
                return NotFound();
            }
            return Ok(serverCredentials);
        }

        [HttpPost]
        public IHttpActionResult InsertServer(Server server)
        {
            bool status = false;
            ServerConfig serverConfig = new ServerConfig { 
                ServerId = server.ServerId,
                ServerName = server.ServerName,
                IPAddress = server.IPAddress,
                RAM = server.RAM,
                Processer = server.Processer,
                OS = server.OS,
                Version = server.Version,
                OrganisationId = server.OrganisationId
            };
            bool ConfigStatus = _serverManager.InsertServerConfig(serverConfig);
            if(ConfigStatus == true)
            {
                ServerCredential serverCredential = new ServerCredential { 
                    HostName = server.HostName,
                    Password = server.Password,
                    ConnectionString = server.ConnectionString
                };
                List<ServerConfig> serverConfigs = _serverManager.GetServerConfigs();
                serverCredential.ServerId = serverConfigs.Last().ServerId;
                bool CredentialStatus = _serverManager.InsertServerCredential(serverCredential);
                if(CredentialStatus == true)
                {
                    status = true;
                }
            }
            if(status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public IHttpActionResult UpdateServer(Server server)
        {
            bool status = false;
            ServerConfig serverConfig = new ServerConfig
            {
                ServerId = server.ServerId,
                ServerName = server.ServerName,
                IPAddress = server.IPAddress,
                RAM = server.RAM,
                Processer = server.Processer,
                OS = server.OS,
                Version = server.Version
            };
            bool ConfigStatus = _serverManager.UpdateServerConfig(serverConfig);
            if (ConfigStatus == true)
            {
                ServerCredential serverCredential = new ServerCredential
                {
                    ServerId = serverConfig.ServerId,
                    HostName = server.HostName,
                    Password = server.Password,
                    ConnectionString = server.ConnectionString
                };
                bool CredentialStatus = _serverManager.UpdateServerCredential(serverCredential);
                if (CredentialStatus == true)
                {
                    status = true;
                }
            }
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public IHttpActionResult DeleteServer(int id)
        {
            bool status = false;
            ServerConfig serverConfig = _serverManager.GetServerConfig(id);
            bool delete = _serverManager.DeleteServerCredential(serverConfig.ServerCredentials.First().ServerCredentialsId);
            if(delete == true)
            {
                bool deleteConfirm = _serverManager.DeleteServerConfig(id);
                if(deleteConfirm == true)
                {
                    status = true;
                }
            }
            if(status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }
    }
}
