using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.UI.Models
{
    public class UserToken
    {
        public int TokenId { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
    }
}