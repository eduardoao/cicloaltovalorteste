using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Model.Entities;
using Dharma.Repository.SQL;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface ICampanhaProdutoRepository : ISQLRepository<CampanhaProduto>
    {
        Task<CampanhaProduto> FindByCampaignAndProductIdAsync(int campanhaId, int produtoId);
        Task<bool> UpdateAsync(CampanhaProduto entity);
        Task<CampanhaProduto> InsertAsync(CampanhaProduto entity);
        Task<bool> EditAsync(CampanhaProduto modelCampanhaProduto);
    }
}
