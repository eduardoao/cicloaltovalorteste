using System.Data;
using Dharma.Repository.SQL;

namespace CicloVidaAltoValor.Application.Repository.Contexts
{
    public class DotzAppContext : ConnectionContext
    {
        public DotzAppContext(IDbConnection dbConnection) : base(dbConnection)
        {
        }
    }
}
