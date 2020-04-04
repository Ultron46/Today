using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Common
{
    public static class Helpers
    {
        public static string GenerateToken(int id,string password)
        {
            string token = id + ":" + password.GetHashCode() + DateTime.Now.GetHashCode().ToString();
            return token;
        }

        public static string Hash(string password)
        {
            return password.GetHashCode().ToString();
        }
    }
}
