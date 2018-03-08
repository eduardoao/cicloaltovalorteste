using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface ICampanhaFaseRepository : ISQLRepository<CampanhaFase>
    {
        Task<bool> RedemptionAlreadyUseAsync(int campanhaId, int campanhaFaseId, DateTime dataInicialResgate, DateTime dataFinalResgate);

        Task<bool> UpdateAsync(CampanhaFase entity);

        Task<CampanhaFase> GetByIdAsync(int id);

        Task<IEnumerable<CampanhaFase>> GetAllByCampaignIdAsync(int campanhaId);

        Task<bool> IsCurrentAsync(int campanhaId);

        Task<CampanhaFase> GetCurrentRedemptionAsync(int campanhaId);

        Task<CampanhaFase> GetByPeriodAsync(DateTime period);

        Task<CampanhaFase> GetCurrentAsync(int campanhaId);

        Task<CampanhaFase> GetCurrentAsync(string nome);

        Task<bool> PeriodAlreadyUseAsync(int campanhaId, int campanhaFaseId, DateTime periodo);

        Task<bool> RedemptionRangeInCampaignRangeAsync(int campanhaId, int campanhaFaseId, DateTime dataInicioResgate, DateTime dataFimResgate);

        Task<CampanhaFase> GetLastFaseAsync(int campaignId);

        Task<CampanhaFase> GetLastFaseAsync(string nome);

        Task<CampanhaFase> GetCurrentSpentPeriodAsync();

        Task<bool> IsOverFase(int campanhaId);
    }
}
