using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Model.Entities;
using Dharma.Repository.SQL;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IUsuarioAcessoRepository : ISQLRepository<UsuarioAcesso>
    {
        Task<UsuarioAcesso> InsertAsync(UsuarioAcesso entity);
    }
}
