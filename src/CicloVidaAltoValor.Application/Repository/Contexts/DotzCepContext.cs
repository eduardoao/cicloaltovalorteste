using Dharma.Repository.SQL;
using System.Data;
namespace CicloVidaAltoValor.Application.Repository.Contexts
{
    /// <summary>
    /// Contexto de conexão com o banco de dados DOTZCEP.
    /// </summary>
    public class DotzCepContext : ConnectionContext
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="dbConnection">Conexão com o banco de dados.</param>
        public DotzCepContext(IDbConnection dbConnection) : base(dbConnection)
        {
        }
    }
}