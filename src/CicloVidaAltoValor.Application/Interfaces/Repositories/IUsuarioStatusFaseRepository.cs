using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IUsuarioStatusFaseRepository : ISQLRepository<UsuarioStatusFase>
    {
        Task<bool> UpdateAsync(UsuarioStatusFase entity);

        Task<UsuarioStatusFase> InsertAsync(UsuarioStatusFase entity);

        Task<UsuarioStatusFase> GetByUserIdAsync(int userId);
        Task<UsuarioStatusFase> GetByUserIdAndCampaignActiveAsync(int userId, int campaignId);

        Task<UsuarioStatusFase> GetByUserIdAndCampaignAndCampaignFaseActiveAsync(int userId, int campaignId,
            int campaignFaseId);

        Task<UsuarioStatusFase> GetByCampaignFaseIdAndUserIdActiveAsync(int campaignFaseId,int userId);
        Task FixUserStatusAsync(int campanhaId);
    }
}
