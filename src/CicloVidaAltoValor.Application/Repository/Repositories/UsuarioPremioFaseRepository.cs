using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dharma.Repository.SQL;
using Dommel;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class UsuarioPremioFaseRepository : SQLRepository<UsuarioPremioFase>, IUsuarioPremioFaseRepository
    {
        public UsuarioPremioFaseRepository(DotzAppContext context) : base(context)
        {
        }

        public Task<IEnumerable<UsuarioPremioFase>> GetAllProductsByUserIdCampaignFaseAsync(int userId, int campaignFaseId)
        {
            const string sql = @"SELECT * 
                                FROM CPR_USUARIO_PREMIO_FASE UP (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK) ON UP.CPR_CAMPANHA_FASE_ID = PF.CPR_CAMPANHA_FASE_ID AND UP.CPR_CAMPANHA_PRODUTO_ID = PF.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON UP.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                WHERE 
                                     UP.CPR_USUARIO_ID = @userId AND UP.CPR_CAMPANHA_FASE_ID = @campaignFaseId ;";

            return this.Connection.SqlConnection.QueryAsync<UsuarioPremioFase, CampanhaProdutoFase, CampanhaProduto, Produto, UsuarioPremioFase>(sql, map:
                  (fase, produtoFase, campanhaProduto, produto) =>
                  {
                      fase.CampanhaProdutoFase = produtoFase;
                      campanhaProduto.Produto = produto;
                      fase.CampanhaProduto = campanhaProduto;
                      return fase;
                  },
                  splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID",
                  param: new { userId, campaignFaseId });

        }

        public Task<IEnumerable<UsuarioPremioFase>> GetAllProductsByUserIdAsync(int userId, int campanhaId)
        {
            /*
            const string sql = @"SELECT * 
                                FROM CPR_USUARIO_PREMIO_FASE UP (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_FASE CF (NOLOCK) ON UP.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                CROSS APPLY
											(
											SELECT  TOP 1 *
											FROM    CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
											WHERE   UP.CPR_CAMPANHA_FASE_ID = PF.CPR_CAMPANHA_FASE_ID AND UP.CPR_CAMPANHA_PRODUTO_ID = PF.CPR_CAMPANHA_PRODUTO_ID
											) PF
                                --INNER JOIN CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK) ON UP.CPR_CAMPANHA_FASE_ID = PF.CPR_CAMPANHA_FASE_ID AND UP.CPR_CAMPANHA_PRODUTO_ID = PF.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON UP.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                WHERE 
                                     UP.CPR_USUARIO_ID = @userId 
                                     AND CP.CPR_CAMPANHA_ID = @campanhaId;";
            */
            const string sql = @"SELECT * 
                                FROM CPR_USUARIO_PREMIO_FASE UP (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_FASE CF (NOLOCK) ON UP.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                INNER JOIN CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK) ON UP.CPR_CAMPANHA_PRODUTO_FASE_ID = PF.CPR_CAMPANHA_PRODUTO_FASE_ID 
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON UP.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                WHERE 
                                     UP.CPR_USUARIO_ID = @userId 
                                     AND CP.CPR_CAMPANHA_ID = @campanhaId;";


            return this.Connection.SqlConnection.QueryAsync<UsuarioPremioFase, CampanhaFase, CampanhaProdutoFase, CampanhaProduto, Produto, UsuarioPremioFase>(sql, map:
                      (fase, campanhaFase, produtoFase, campanhaProduto, produto) =>
                      {
                          fase.CampanhaFase = campanhaFase;
                          fase.CampanhaProdutoFase = produtoFase;
                          campanhaProduto.Produto = produto;
                          fase.CampanhaProduto = campanhaProduto;
                          return fase;
                      },
                      splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID",
                      param: new { userId, campanhaId });

        }

        public Task<bool> HasPrize(int usuarioId, int campanhaFaseId)
        {
            const string sql = @"SELECT 
                                    1
                                FROM CPR_USUARIO_PREMIO_FASE (NOLOCK)
                                WHERE
                                     CPR_CAMPANHA_FASE_ID = @campanhaFaseId
                                     AND CPR_USUARIO_ID = @usuarioId;";

            return this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaFaseId, usuarioId });
        }

        public async Task<bool> UpdateAsync(UsuarioPremioFase entity)
        {
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<UsuarioPremioFase> InsertAsync(UsuarioPremioFase entity)
        {
            entity.DataCriacao = DateTime.Now;
            entity.UsuarioPremioFaseId = Convert.ToInt32(await Connection.SqlConnection.InsertAsync(entity, this.Connection.CurrentTransaction));
            return entity;
        }
    }
}
