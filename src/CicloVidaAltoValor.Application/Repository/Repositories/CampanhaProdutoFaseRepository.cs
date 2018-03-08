using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dharma.Repository.SQL;
using Dommel;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class CampanhaProdutoFaseRepository : SQLRepository<CampanhaProdutoFase>, ICampanhaProdutoFaseRepository
    {
        public CampanhaProdutoFaseRepository(DotzAppContext context) : base(context)
        {
        }


        public async Task<IEnumerable<CampanhaProdutoFase>> GetAllVoltsAsync(int campanhaId, string carteira, int produtoId, int campanhaFaseId, FaixaMeta faixaMeta)
        {
            const string sql = @"SELECT 
                                        * 
                                 FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                    INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) CF ON PF.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                    INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                    INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
								    INNER JOIN WMS_PRODUTO_EQUIVALENCIA (NOLOCK) EQ ON  P.PRODUTO_ID IN (EQ.PRODUTO_ID_PRINCIPAL,EQ.PRODUTO_ID_EQUIVALENTE) AND EQ.PRODUTO_ID_PRINCIPAL <> EQ.PRODUTO_ID_EQUIVALENTE
								 WHERE 
                                      CF.CPR_CAMPANHA_ID = @campanhaId  
                                      AND PF.CPR_CAMPANHA_FASE_ID = @campanhaFaseId
                                      AND EQ.PRODUTO_ID_PRINCIPAL = @produtoId 
                                      AND PF.FAIXA_META = @faixaMeta
                                      AND PF.CARTEIRA = @carteira  
                                      AND PF.VOLTAGEM IS NOT NULL  
                                      AND NOT EXISTS (SELECT TOP 1 1  FROM CPR_CAMPANHA_PRODUTO_FASE (NOLOCK) A 
													                  INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) B ON A.CPR_CAMPANHA_FASE_ID = B.CPR_CAMPANHA_FASE_ID
													                  WHERE 
                                                                            A.CPR_CAMPANHA_PRODUTO_ID = PF.CPR_CAMPANHA_PRODUTO_ID    
                                                                            AND B.CPR_CAMPANHA_ID = CF.CPR_CAMPANHA_ID  
                                                                            AND B.CPR_CAMPANHA_FASE_ID = PF.CPR_CAMPANHA_FASE_ID
                                                                            AND A.FAIXA_META = PF.FAIXA_META 
																		    AND A.CATALOGO = PF.CATALOGO 
																		    AND A.CARTEIRA = PF.CARTEIRA
																		    AND A.VOLTAGEM <> PF.VOLTAGEM  );";
            var p =
                new
                {
                    campanhaId,
                    campanhaFaseId,
                    produtoId,
                    carteira = new DbString() { Value = faixaMeta.ToString(), Length = 60, IsAnsi = true },
                    faixaMeta = new DbString() { Value = faixaMeta.ToString(), Length = 80, IsAnsi = true }
                };
            return await this.Connection.SqlConnection.QueryAsync<CampanhaProdutoFase, CampanhaFase, CampanhaProduto, Produto, CampanhaProdutoFase>(sql, map:
               (produtoFase, fase, campanhaProduto, produto) =>
               {
                   campanhaProduto.Produto = produto;
                   produtoFase.CampanhaFase = fase;
                   produtoFase.CampanhaProduto = campanhaProduto;
                   return produtoFase;
               },
               splitOn: "CPR_CAMPANHA_PRODUTO_FASE_ID,CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID",
               param: p);
        }

        public async Task<IEnumerable<CampanhaProdutoFase>> GetAllAndNextProductsByCampaignFaseAsync(int campanhaId, int campanhaFaseId, string carteira, FaixaMeta faixaMeta)
        {

            const string sql = @"SELECT * 
                                FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) CF ON PF.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                INNER JOIN CPR_CAMPANHA C (NOLOCK) ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                    PF.CPR_CAMPANHA_FASE_ID = @campanhaFaseId
                                    AND C.CPR_CAMPANHA_ID = @campanhaId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
	                                AND C.ATIVO  = 1
                                    AND PF.CARTEIRA = @carteira
                                    AND PF.FAIXA_META = @faixaMeta
                                    AND (PF.VOLTAGEM IS NULL OR EXISTS(SELECT TOP 1 1 FROM WMS_PRODUTO_EQUIVALENCIA EQ WHERE  P.PRODUTO_ID = EQ.PRODUTO_ID_EQUIVALENTE AND EQ.PRODUTO_ID_PRINCIPAL = P.PRODUTO_ID))
                                ORDER BY PF.PERIODO, P.NOME;";
            var p =
                new
                {
                    campanhaFaseId,
                    campanhaId,
                    carteira = new DbString() { Value = carteira, Length = 80, IsAnsi = true },
                    faixaMeta = new DbString() { Value = faixaMeta.ToString(), Length = 80, IsAnsi = true }
                };

            return await this.Connection.SqlConnection.QueryAsync<CampanhaProdutoFase, CampanhaFase, CampanhaProduto, Produto, Campanha, CampanhaProdutoFase>(sql, map:
                  (produtoFase, fase, campanhaProduto, produto, campanha) =>
                  {
                      campanhaProduto.Produto = produto;
                      produtoFase.CampanhaFase = fase;
                      produtoFase.CampanhaFase.Campanha = campanha;
                      produtoFase.CampanhaProduto = campanhaProduto;
                      return produtoFase;
                  },
                  splitOn: "CPR_CAMPANHA_PRODUTO_FASE_ID,CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID,CPR_CAMPANHA_ID",
                  param: p);
        }


        public async Task<IEnumerable<CampanhaProdutoFase>> GetAllCurrentProductsAsync(int campanhaId, string carteira, FaixaMeta faixaMeta)
        {
            const string sql = @"SELECT * 
                                FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) CF ON PF.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                INNER JOIN CPR_CAMPANHA C (NOLOCK) ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                    C.CPR_CAMPANHA_ID = @campanhaId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
	                                AND MONTH(CF.PERIODO) = MONTH(GETDATE()) AND YEAR(CF.PERIODO) = YEAR(GETDATE())
                                    AND MONTH(PF.PERIODO) = MONTH(GETDATE()) AND YEAR(PF.PERIODO) = YEAR(GETDATE())
	                                AND C.ATIVO  = 1
                                    AND PF.CARTEIRA = @carteira
                                    AND PF.FAIXA_META = @faixaMeta
                                    AND (PF.VOLTAGEM IS NULL OR EXISTS(SELECT TOP 1 1 FROM WMS_PRODUTO_EQUIVALENCIA EQ WHERE  P.PRODUTO_ID = EQ.PRODUTO_ID_EQUIVALENTE AND EQ.PRODUTO_ID_PRINCIPAL = P.PRODUTO_ID))
                                ORDER BY PF.PERIODO, P.NOME;";

            var p =
                new
                {
                    campanhaId,
                    carteira = new DbString() { Value = carteira, Length = 80, IsAnsi = true },
                    faixaMeta = new DbString() { Value = faixaMeta.ToString(), Length = 80, IsAnsi = true }
                };
            return await this.Connection.SqlConnection.QueryAsync<CampanhaProdutoFase, CampanhaFase, CampanhaProduto, Produto, Campanha, CampanhaProdutoFase>(sql, map:
                  (produtoFase, fase, campanhaProduto, produto, campanha) =>
                  {
                      campanhaProduto.Produto = produto;
                      produtoFase.CampanhaFase = fase;
                      produtoFase.CampanhaFase.Campanha = campanha;
                      produtoFase.CampanhaProduto = campanhaProduto;
                      return produtoFase;
                  },
                  splitOn: "CPR_CAMPANHA_PRODUTO_FASE_ID,CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID,CPR_CAMPANHA_ID",
                  param: p);
        }

        public async Task<IEnumerable<CampanhaProdutoFase>> GetAllCurrentProductsAsync(int campanhaId, string carteira, int campanhaFaseId, FaixaMeta faixaMeta)
        {

            const string sql = @"SELECT * 
                                FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) CF ON PF.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                INNER JOIN CPR_CAMPANHA C (NOLOCK) ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                    C.CPR_CAMPANHA_ID = @campanhaId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
                                    AND PF.CPR_CAMPANHA_FASE_ID >= @campanhaFaseId
	                                AND C.ATIVO  = 1
                                    AND PF.CARTEIRA = @carteira
                                    AND PF.FAIXA_META = @faixaMeta
                                    AND (PF.VOLTAGEM IS NULL OR EXISTS(SELECT TOP 1 1 FROM WMS_PRODUTO_EQUIVALENCIA EQ WHERE  P.PRODUTO_ID = EQ.PRODUTO_ID_EQUIVALENTE AND EQ.PRODUTO_ID_PRINCIPAL = P.PRODUTO_ID))
                                ORDER BY PF.PERIODO, P.NOME;";

            var p =
                new
                {
                    campanhaId,
                    carteira = new DbString() { Value = carteira, Length = 80, IsAnsi = true },
                    campanhaFaseId,
                    faixaMeta = new DbString() { Value = faixaMeta.ToString(), Length = 80, IsAnsi = true }
                };

            return await this.Connection.SqlConnection.QueryAsync<CampanhaProdutoFase, CampanhaFase, CampanhaProduto, Produto, Campanha, CampanhaProdutoFase>(sql, map:
                  (produtoFase, fase, campanhaProduto, produto, campanha) =>
                  {
                      campanhaProduto.Produto = produto;
                      produtoFase.CampanhaFase = fase;
                      produtoFase.CampanhaFase.Campanha = campanha;
                      produtoFase.CampanhaProduto = campanhaProduto;
                      return produtoFase;
                  },
                  splitOn: "CPR_CAMPANHA_PRODUTO_FASE_ID,CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID,CPR_CAMPANHA_ID",
                  param: p);
        }

        public async Task<IEnumerable<CampanhaProdutoFase>> GetAllAndNextProductsAsync(int campanhaId, string carteira, FaixaMeta faixaMeta)
        {
            const string sql = @"SELECT * 
                                FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) CF ON PF.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                INNER JOIN CPR_CAMPANHA C (NOLOCK) ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                    C.CPR_CAMPANHA_ID = @campanhaId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
                                    AND CF.PERIODO >= (SELECT DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0))
	                                AND C.ATIVO  = 1
                                    AND PF.CARTEIRA = @carteira
                                    AND PF.FAIXA_META = @faixaMeta
                                    AND (PF.VOLTAGEM IS NULL OR EXISTS(SELECT TOP 1 1 FROM WMS_PRODUTO_EQUIVALENCIA EQ WHERE  P.PRODUTO_ID = EQ.PRODUTO_ID_EQUIVALENTE AND EQ.PRODUTO_ID_PRINCIPAL = P.PRODUTO_ID))
                                ORDER BY PF.PERIODO, P.NOME;";

            var p =
                new
                {
                    campanhaId,
                    carteira = new DbString() { Value = carteira, Length = 80, IsAnsi = true },
                    faixaMeta = new DbString() { Value = faixaMeta.ToString(), Length = 80, IsAnsi = true }
                };

            return await this.Connection.SqlConnection.QueryAsync<CampanhaProdutoFase, CampanhaFase, CampanhaProduto, Produto, Campanha, CampanhaProdutoFase>(sql, map:
                  (produtoFase, fase, campanhaProduto, produto, campanha) =>
                  {
                      campanhaProduto.Produto = produto;
                      produtoFase.CampanhaFase = fase;
                      produtoFase.CampanhaFase.Campanha = campanha;
                      produtoFase.CampanhaProduto = campanhaProduto;
                      return produtoFase;
                  },
                  splitOn: "CPR_CAMPANHA_PRODUTO_FASE_ID,CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID,CPR_CAMPANHA_ID",
                  param: p);

        }

        public async Task<IEnumerable<CampanhaProdutoFase>> GetAllAndNextProductsAsync(int campanhaId, string carteira, int campanhaFaseId, FaixaMeta faixaMeta)
        {
            const string sql = @"SELECT * 
                                FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) CF ON PF.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                INNER JOIN CPR_CAMPANHA C (NOLOCK) ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                    C.CPR_CAMPANHA_ID = @campanhaId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
                                    AND PF.CPR_CAMPANHA_FASE_ID >= @campanhaFaseId
	                                AND C.ATIVO  = 1
                                    AND PF.CARTEIRA = @carteira
                                    AND PF.FAIXA_META = @faixaMeta
                                    AND (PF.VOLTAGEM IS NULL OR EXISTS(SELECT TOP 1 1 FROM WMS_PRODUTO_EQUIVALENCIA EQ WHERE  P.PRODUTO_ID = EQ.PRODUTO_ID_EQUIVALENTE AND EQ.PRODUTO_ID_PRINCIPAL = P.PRODUTO_ID))
                                ORDER BY PF.PERIODO, P.NOME;";
            var p =
                new
                {
                    campanhaId,
                    campanhaFaseId,
                    carteira = new DbString() { Value = carteira, Length = 80, IsAnsi = true },
                    faixaMeta = new DbString() { Value = faixaMeta.ToString(), Length = 80, IsAnsi = true }
                };

            return await this.Connection.SqlConnection.QueryAsync<CampanhaProdutoFase, CampanhaFase, CampanhaProduto, Produto, Campanha, CampanhaProdutoFase>(sql, map:
                  (produtoFase, fase, campanhaProduto, produto, campanha) =>
                  {
                      campanhaProduto.Produto = produto;
                      produtoFase.CampanhaFase = fase;
                      produtoFase.CampanhaFase.Campanha = campanha;
                      produtoFase.CampanhaProduto = campanhaProduto;
                      return produtoFase;
                  },
                  splitOn: "CPR_CAMPANHA_PRODUTO_FASE_ID,CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID,CPR_CAMPANHA_ID",
                  param: p);

        }

        public async Task<CampanhaProdutoFase> FindByCampaignProductIdAndCampaignFaseIdAsync(int campanhaProdutoId, int campanhaFaseId)
        {
            const string sql = @"SELECT 
                                    *
                                FROM CPR_CAMPANHA_PRODUTO_FASE (NOLOCK)
                                WHERE CPR_CAMPANHA_FASE_ID = @campanhaFaseId AND CPR_CAMPANHA_PRODUTO_ID = @campanhaProdutoId;";

            return await Connection.SqlConnection.QueryFirstOrDefaultAsync<CampanhaProdutoFase>(sql, new { campanhaProdutoId, campanhaFaseId });
        }

        public async Task<CampanhaProdutoFase> FindByKeysAsync(int campanhaProdutoId, int campanhaFaseId, string faixaMeta, string carteira, string catalogo)
        {
            const string sql = @"SELECT 
                                    *
                                FROM CPR_CAMPANHA_PRODUTO_FASE (NOLOCK)
                                WHERE CPR_CAMPANHA_FASE_ID = @campanhaFaseId AND CPR_CAMPANHA_PRODUTO_ID = @campanhaProdutoId AND FAIXA_META = @faixaMeta AND CARTEIRA = @carteira AND CATALOGO = @catalogo ;";

            var parms = new
            {
                campanhaFaseId,
                campanhaProdutoId,
                carteira = new DbString() { Value = carteira, Length = 80, IsAnsi = true },
                faixaMeta = new DbString() { Value = faixaMeta, Length = 80, IsAnsi = true },
                catalogo = new DbString() { Value = catalogo, Length = 80, IsAnsi = true }
            };

            return await Connection.SqlConnection.QueryFirstOrDefaultAsync<CampanhaProdutoFase>(sql, parms);
        }

        public async Task<bool> ExistByUserIdAndCampaignProductIdAsync(int usuarioId, int campanhaFaseId)
        {
            const string sql = @"SELECT 
                                    1
                                FROM CPR_CAMPANHA_PRODUTO_FASE (NOLOCK)
                                WHERE CPR_CAMPANHA_FASE_ID = @campanhaFaseId AND CPR_USUARIO_ID = @usuarioId;";

            return await Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaFaseId, usuarioId });
        }

        public async Task<IEnumerable<CampanhaProdutoFase>> GetAllAsync(int campanhaFaseId)
        {
            const string sql = @"SELECT * 
                                FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) CF ON PF.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                INNER JOIN CPR_CAMPANHA C (NOLOCK) ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                LEFT JOIN DBS_PARCEIRO PA ON C.USUARIO_ID_PARCEIRO = PA.USUARIO_ID_PARCEIRO
                                WHERE 
                                    PF.CPR_CAMPANHA_FASE_ID = @campanhaFaseId";

            return await this.Connection.SqlConnection.QueryAsync<CampanhaProdutoFase, CampanhaFase, CampanhaProduto, Produto, Campanha, Parceiro, CampanhaProdutoFase>(sql, map:
                  (produtoFase, fase, campanhaProduto, produto, campanha, parceiro) =>
                  {
                      campanha.Parceiro = parceiro;
                      campanhaProduto.Produto = produto;
                      produtoFase.CampanhaFase = fase;
                      produtoFase.CampanhaFase.Campanha = campanha;
                      produtoFase.CampanhaProduto = campanhaProduto;
                      return produtoFase;
                  },
                  splitOn: "CPR_CAMPANHA_PRODUTO_FASE_ID,CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID,CPR_CAMPANHA_ID,USUARIO_ID_PARCEIRO",
                  param: new { campanhaFaseId });
        }

        public async Task<bool> ExistByCampaignProductIdAndCampaignFaseIdAsync(int campanhaProdutoId, int campanhaFaseId)
        {
            const string sql = @"SELECT 
                                    1
                                FROM CPR_CAMPANHA_PRODUTO_FASE (NOLOCK)
                                WHERE CPR_CAMPANHA_FASE_ID = @campanhaFaseId AND CPR_CAMPANHA_PRODUTO_ID = @campanhaProdutoId;";

            return await Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaFaseId, campanhaProdutoId });
        }

        public async Task<bool> ExistByCampaignProductIdAndProductIdAndCampaignFaseIdAsync(int campanhaProdutoId, int produtoId, int campanhaFaseId)
        {
            const string sql = @"SELECT 
                                    1
                                FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                WHERE PF.CPR_CAMPANHA_FASE_ID = @campanhaFaseId AND P.PRODUTO_ID = @produtoId AND PF.CPR_CAMPANHA_PRODUTO_ID = @campanhaProdutoId;";

            return await Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaFaseId, produtoId, campanhaProdutoId });
        }

        public async Task<bool> ExistByIdAndCampaignProductIdAndProductIdAndCampaignFaseIdAsync(int campanhaProdutoFaseId, int campanhaProdutoId, int produtoId, int campanhaFaseId)
        {
            const string sql = @"SELECT 
                                    1
                                FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                WHERE 
                                     PF.CPR_CAMPANHA_PRODUTO_FASE_ID = @campanhaProdutoFaseId
                                     AND PF.CPR_CAMPANHA_FASE_ID = @campanhaFaseId 
                                     AND P.PRODUTO_ID = @produtoId 
                                     AND PF.CPR_CAMPANHA_PRODUTO_ID = @campanhaProdutoId;";

            return await Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaFaseId, produtoId, campanhaProdutoId, campanhaProdutoFaseId });
        }

        public async Task<bool> UpdateAsync(CampanhaProdutoFase entity)
        {
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<CampanhaProdutoFase> InsertAsync(CampanhaProdutoFase entity)
        {
            entity.DataCriacao = DateTime.Now;
            entity.CampanhaProdutoFaseId = Convert.ToInt32(await Connection.SqlConnection.InsertAsync(entity));
            return entity;
        }


        public async Task<CampanhaProdutoFase> GetProductByCampaignProductIdAndCampaignFaseAsync(int campanhaProdutoId, int campaignFaseId)
        {
            const string sql = @"SELECT * 
                                FROM CPR_CAMPANHA_PRODUTO_FASE PF (NOLOCK)
                                INNER JOIN CPR_CAMPANHA_PRODUTO CP (NOLOCK) ON PF.CPR_CAMPANHA_PRODUTO_ID = CP.CPR_CAMPANHA_PRODUTO_ID
                                INNER JOIN WMS_PRODUTO P (NOLOCK) ON CP.WMS_PRODUTO_ID = P.PRODUTO_ID
                                WHERE 
                                     PF.CPR_CAMPANHA_PRODUTO_ID = @campanhaProdutoId AND PF.CPR_CAMPANHA_FASE_ID = @campaignFaseId ;";

            return (await this.Connection.SqlConnection.QueryAsync<CampanhaProdutoFase, CampanhaProduto, Produto, CampanhaProdutoFase>(sql, map:
                  (produtoFase, campanhaProduto, produto) =>
                  {
                      campanhaProduto.Produto = produto;
                      produtoFase.CampanhaProduto = campanhaProduto;
                      return produtoFase;
                  },
                  splitOn: "CPR_CAMPANHA_PRODUTO_FASE_ID,CPR_CAMPANHA_PRODUTO_ID,PRODUTO_ID",
                  param: new { campanhaProdutoId, campaignFaseId })).FirstOrDefault();

        }
    }
}
