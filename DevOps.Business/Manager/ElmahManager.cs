using DevOps.Business.Interfaces;
using DevOps.Data.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Manager
{
    public class ElmahManager : IElmahManager
    {
        IElmahDataReposiotry _elmahDataReposiotry;

        public ElmahManager(IElmahDataReposiotry elmahDataReposiotry)
        {
            _elmahDataReposiotry = elmahDataReposiotry;
        }

        public ELMAH_Error GerErroByID(Guid id)
        {
            return _elmahDataReposiotry.GerErroByID(id);
        }

        public List<ELMAH_Error> GetELMAH_Errors()
        {
            return _elmahDataReposiotry.GetELMAH_Errors();
        }
    }
}
