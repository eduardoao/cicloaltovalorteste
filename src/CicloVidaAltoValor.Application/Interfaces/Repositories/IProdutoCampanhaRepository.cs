using CicloVidaAltoValor.Application.Model.Entities;
using Dharma.Repository.SQL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IProdutoCampanhaRepository : ISQLRepository<ProdutoCampanha>
    {
        Task<IEnumerable<ProdutoCampanha>> GetAllByCampaignIdAsync(int campanhaId, string carteira);
        Task<ProdutoCampanha> GetByCampaignIdAsync(int campanhaId);

        Task<ProdutoCampanha> FindByAsync(int produtoCampanhaId, int campanhaProdutoId, int produtoId, int campanhaId, string carteira);
    }
}
