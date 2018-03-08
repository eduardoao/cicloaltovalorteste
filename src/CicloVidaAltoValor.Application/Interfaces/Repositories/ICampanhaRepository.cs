using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Model.Entities;
using Dharma.Repository.SQL;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface ICampanhaRepository : ISQLRepository<Campanha>
    {
        Task<bool> IsCurrentAsync(string nome);
        Task<bool> IsCurrentAsync(int campanhaId);

        Task<Campanha> GetCurrentAsync(string nome);
        Task<Campanha> GetCurrentAsync(int campanhaId);

        Task<IEnumerable<Campanha>> GetAllAsync();

        Task<IEnumerable<Campanha>> GetAllActiveAsync();


        Task<Campanha> GetByIdAsync(int id);

        Task<Campanha> GetByIdIncludeFasesAsync(int id);

        Task<Campanha> GetByIdIncludeFasesAndPrevisaoFaturaAsync(int id);

        Task<bool> UpdateAsync(Campanha entity);
        Task<int> GetTotalUsersAsync(int campanhaId);
        Task<int> GetTotalPricesAsync(int campanhaId);
        Task<bool> RangeDateConflictRedemptionDateAsync(int campanhaId, DateTime dataInicio, DateTime dataFim);
    }
}
