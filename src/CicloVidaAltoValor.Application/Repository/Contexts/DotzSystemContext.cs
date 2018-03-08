using System.Data;
using Dharma.Repository.SQL;

namespace CicloVidaAltoValor.Application.Repository.Contexts
{
    public class DotzSystemContext : ConnectionContext
    {
        public DotzSystemContext(IDbConnection dbConnection) : base(dbConnection)
        {
        }
    }
}
