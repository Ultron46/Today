using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class ServerConfig
    {
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public string IPAddress { get; set; }
        public string RAM { get; set; }
        public string Processer { get; set; }
        public string OS { get; set; }
        public string Version { get; set; }
        public Nullable<int> OrganisationId { get; set; }
    }
}