using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class ServerCredential
    {
        public int ServerCredentialsId { get; set; }
        public Nullable<int> ServerId { get; set; }
        public string HostName { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }
    }
}