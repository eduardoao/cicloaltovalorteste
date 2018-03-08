using System.Collections.Generic;
using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IUsuarioPremioFaseRepository : ISQLRepository<UsuarioPremioFase>
    {
        Task<IEnumerable<UsuarioPremioFase>>  GetAllProductsByUserIdCampaignFaseAsync(int usuarioId, int campanhaFaseId);

        Task<IEnumerable<UsuarioPremioFase>> GetAllProductsByUserIdAsync(int usuarioId, int campanhaId);

        Task<bool> HasPrize(int usuarioId, int campanhaFaseId);

        Task<bool> UpdateAsync(UsuarioPremioFase entity);

        Task<UsuarioPremioFase> InsertAsync(UsuarioPremioFase entity);
    }
}
