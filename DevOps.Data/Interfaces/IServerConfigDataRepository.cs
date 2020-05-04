﻿using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.Interfaces
{
    public interface IServerConfigDataRepository
    {
        List<ServerConfig> GetServerConfigs(int id);
        ServerConfig GetServerConfig(int id);
        bool InsertServerConfig(ServerConfig serverConfig);
        bool UpdateServerConfig(ServerConfig serverConfig);
        bool DeleteServerConfig(int id);
        bool InsertServerBuild(int BuildId, int ServerId, int UserId);
        List<ServerBuild> serverBuilds(int id);
    }
}
