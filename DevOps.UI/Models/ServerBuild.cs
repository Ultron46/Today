using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class ServerBuild
    {
        public int ServerBuildId { get; set; }
        public Nullable<int> BuildId { get; set; }
        public Nullable<int> ServerId { get; set; }
        public Nullable<int> PublishedBy { get; set; }
        public Nullable<byte> Mejor_Version { get; set; }
        public Nullable<byte> Minor_Version { get; set; }
        public Nullable<int> Build_Version { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public BuildProject BuildProject { get; set; }
        public string Status { get; set; }
    }
}