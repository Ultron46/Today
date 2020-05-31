using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.DataRepository
{
    public class ElmahDataReposiotry : IElmahDataReposiotry
    {
        DevOpsEntities DbContext;
        public ElmahDataReposiotry()
        {
            DbContext = new DevOpsEntities();
        }

        public ELMAH_Error GerErroByID(Guid id)
        {
            try
            {
                return DbContext.ELMAH_Error.FirstOrDefault(e => e.ErrorId == id);
            }
            catch
            {
                return null;
            }
        }

        public List<ELMAH_Error> GetELMAH_Errors()
        {
            try
            {
                return DbContext.ELMAH_Error.OrderByDescending(x => x.TimeUtc).Take(100).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
