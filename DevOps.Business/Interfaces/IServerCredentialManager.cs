using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Interfaces
{
    public interface IServerCredentialManager
    {
        List<ServerCredential> GetServerCredentials(int ServerId);
        ServerCredential GetServerCredential(int id);
        bool InsertServerCredential(ServerCredential serverCredential);
        bool UpdateServerCredential(ServerCredential serverCredential);
        bool DeleteServerCredential(int id);
        bool DeleteAllServerCredential(int id);
        bool InsertEmail(EmailMaster emailMaster);
    }
}
