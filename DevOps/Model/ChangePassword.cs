using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.Model
{
    public class ChangePassword
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CurrentPassword { get; set; }
    }
}