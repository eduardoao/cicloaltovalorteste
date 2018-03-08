using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;
using Dharma.Repository.SQL;
using Dommel;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class CampanhaProdutoRepository : SQLRepository<CampanhaProduto>, ICampanhaProdutoRepository
    {
        public CampanhaProdutoRepository(DotzAppContext context) : base(context)
        {
        }
       

        public async Task<CampanhaProduto> FindByCampaignAndProductIdAsync(int campanhaId, int produtoId)
        {
            const string sql = @"SELECT 
                                    *
                                FROM CPR_CAMPANHA_PRODUTO (NOLOCK) A
                                INNER JOIN CPR_CAMPANHA     (NOLOCK) B   ON A.CPR_CAMPANHA_ID = B.CPR_CAMPANHA_ID
                                WHERE A.CPR_CAMPANHA_ID = @campanhaId AND A.WMS_PRODUTO_ID = @produtoId;";

            return (await this.Connection.SqlConnection.QueryAsync<CampanhaProduto, Campanha, CampanhaProduto>(sql, map:((
                produto, campanha) =>
                {
                    produto.Campanha = campanha;
                    return produto;
                }),
            splitOn: "CPR_CAMPANHA_ID", param: new { campanhaId, produtoId })).FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(CampanhaProduto entity)
        {
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<CampanhaProduto> InsertAsync(CampanhaProduto entity)
        {
            entity.CampanhaProdutoId = Convert.ToInt32(await Connection.SqlConnection.InsertAsync(entity, this.Connection.CurrentTransaction));
            return entity;
        }

        public async Task<bool> EditAsync(CampanhaProduto entity)
        {
            return await Connection.SqlConnection.UpdateAsync(entity, this.Connection.CurrentTransaction);
        }
    }
}
