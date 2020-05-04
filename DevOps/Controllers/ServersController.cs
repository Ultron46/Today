using DevOps.Business.Interfaces;
using DevOps.DataEntities.Models;
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
            List<ServerConfig> serverConfigs = _serverManager.GetServerConfigs(Organization);
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

        public IHttpActionResult GetServerCredentials(int ServerId)
        {
            List<ServerCredential> serverCredentials = _serverManager.GetServerCredentials(ServerId);
            if(serverCredentials == null)
            {
                return NotFound();
            }
            return Ok(serverCredentials);
        }

        [HttpPost]
        public IHttpActionResult InsertServer(ServerConfig serverConfig)
        {
            bool status = false;
            bool ConfigStatus = _serverManager.InsertServerConfig(serverConfig);
            if (ConfigStatus == true)
            {
                status = true;
            }
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public IHttpActionResult InsertServerCredentials(ServerCredential serverCredential)
        {
            bool status = _serverManager.InsertServerCredential(serverCredential);
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public IHttpActionResult UpdateServer(ServerConfig serverConfig)
        {
            bool status = false;
            bool ConfigStatus = _serverManager.UpdateServerConfig(serverConfig);
            if (ConfigStatus == true)
            {
                status = true;
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
            bool delete = _serverManager.DeleteAllServerCredential(id);
            if (delete == true)
            {
                bool deleteConfirm = _serverManager.DeleteServerConfig(id);
                if (deleteConfirm == true)
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
        public IHttpActionResult InsertEmail(DataEntities.Models.EmailMaster emails)
        {
            bool status = _serverManager.InsertEmail(emails);
            if (status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpGet]
        public IHttpActionResult InsertServerBuild(int BuildId, int ServerID, int UserId)
        {
            bool status = _serverManager.InsertServerBuild(BuildId, ServerID, UserId);
            if(status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpGet]
        public IHttpActionResult ServerBuild(string downloadURL)
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetServerBuilds(int id)
        {
            List<DataEntities.Models.ServerBuild> serverBuilds = _serverManager.serverBuilds(id);
            if(serverBuilds == null)
            {
                return NotFound();
            }
            return Ok(serverBuilds);
        }
    }
}
