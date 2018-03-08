using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IProdutoRepository : ISQLRepository<Produto>
    {
        Task<bool> IsVoltAsync(int produtoId);
     
    }
}
