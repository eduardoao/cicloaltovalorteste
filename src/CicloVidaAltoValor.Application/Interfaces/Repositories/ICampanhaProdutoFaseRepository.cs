using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface ICampanhaProdutoFaseRepository : ISQLRepository<CampanhaProdutoFase>
    {
        Task<IEnumerable<CampanhaProdutoFase>> GetAllVoltsAsync(int campanhaId, string carteira,int produtoId, int campanhaFaseId, FaixaMeta faixaMeta);

        Task<IEnumerable<CampanhaProdutoFase>> GetAllAndNextProductsByCampaignFaseAsync(int campanhaId, int campanhaFaseId, string carteira, FaixaMeta faixaMeta);

        Task<IEnumerable<CampanhaProdutoFase>> GetAllAndNextProductsAsync(int campanhaId, string carteira, FaixaMeta faixaMeta);

        Task<IEnumerable<CampanhaProdutoFase>> GetAllAndNextProductsAsync(int campanhaId, string carteira, int campanhaFaseId, FaixaMeta faixaMeta);

        Task<IEnumerable<CampanhaProdutoFase>> GetAllCurrentProductsAsync(int campanhaId, string carteira, FaixaMeta faixaMeta);

        Task<IEnumerable<CampanhaProdutoFase>> GetAllCurrentProductsAsync(int campanhaId, string carteira, int campanhaFaseId, FaixaMeta faixaMeta);

        Task<CampanhaProdutoFase> FindByCampaignProductIdAndCampaignFaseIdAsync(int campanhaProdutoId, int campanhaFaseId);

        Task<CampanhaProdutoFase> FindByKeysAsync(int campanhaProdutoId, int campanhaFaseId, string faixaMeta, string carteira, string catalogo);

        Task<CampanhaProdutoFase> GetProductByCampaignProductIdAndCampaignFaseAsync(int campanhaProdutoId, int campaignFaseId);

        Task<bool> ExistByCampaignProductIdAndProductIdAndCampaignFaseIdAsync(int campanhaProdutoId, int produtoId, int campanhaFaseId);

        Task<bool> ExistByCampaignProductIdAndCampaignFaseIdAsync(int campanhaProdutoId, int campanhaFaseId);

        Task<bool> ExistByIdAndCampaignProductIdAndProductIdAndCampaignFaseIdAsync(int campanhaProdutoFaseId,
            int campanhaProdutoId, int produtoId, int campanhaFaseId);

        Task<CampanhaProdutoFase> InsertAsync(CampanhaProdutoFase entity);

        Task<bool> UpdateAsync(CampanhaProdutoFase entity);

        Task<bool> ExistByUserIdAndCampaignProductIdAsync(int usuarioId, int campanhaFaseId);

        Task<IEnumerable<CampanhaProdutoFase>> GetAllAsync(int campanhaFaseId);
    }
}
