using System.Collections.Generic;
using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Contracts.Campanha;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface ICampanhaFaseUsuarioAcessoRepository : ISQLRepository<CampanhaFaseUsuarioAcesso>
    {
        Task<bool> UpdateAsync(CampanhaFaseUsuarioAcesso entity);

        Task<bool> UpdateBonusByUserAsync(int usuarioId);


        Task<bool> ExistAsync(int campanhaFaseId, int usuarioId);

        Task<CampanhaFaseUsuarioAcesso> GetByCampaignFaseAndUserIdAsync(int campanhaFaseId, int usuarioId);

        Task<IEnumerable<CampanhaFaseUsuarioAcesso>> GetAllNotBonusAsync(int campanhaFaseId);

        Task<dynamic> GetAllAccessUsersAndFaseAsync(int campanhaId);


        Task<IEnumerable<Usuario>> GetAllNotBonusAccessFaseThreeTimesAsync(int campanhaId);

    }
}
