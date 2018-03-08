using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IUsuarioStatusRepository : ISQLRepository<UsuarioStatus>
    {
        Task<UsuarioStatus> InsertAsync(UsuarioStatus entity);
        //Task<UsuarioStatus> GetByUserIdAndCampaignActiveAsync(int userId, int campaignId);        
        Task<UsuarioStatus> GetByUserIdStatusActiveAsync(int userId);
        Task FixUserStatusAsync(int campanhaId);
        Task<bool> CanPrizeAsync(int userId);
    }
}
