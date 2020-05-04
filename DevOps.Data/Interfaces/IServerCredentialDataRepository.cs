using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Data.Interfaces
{
    public interface IServerCredentialDataRepository
    {
        List<ServerCredential> GetServerCredentials(int ServerId);
        ServerCredential GetServerCredential(int id);
        bool InsertServerCredential(ServerCredential serverCredential);
        bool UpdateServerCredential(ServerCredential serverCredential);
        bool DeleteServerCredential(int id);
        bool DeleteAllServerCredentials(int id);
        bool InsertEmail(EmailMaster emailMaster);
    }
}
