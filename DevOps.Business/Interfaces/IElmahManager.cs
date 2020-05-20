using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Interfaces
{
    public interface IElmahManager
    {
        List<ELMAH_Error> GetELMAH_Errors();
        ELMAH_Error GerErroByID(Guid id);
    }
}
