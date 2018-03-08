using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;
using Dommel;
using Microsoft.Extensions.Logging;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class CampanhaRepository : SQLRepository<Campanha>, ICampanhaRepository
    {
        private readonly ILogger<CampanhaRepository> _logger;

        public CampanhaRepository(DotzAppContext context, ILogger<CampanhaRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public Task<bool> IsCurrentAsync(string nome)
        {
            const string sql = @"SELECT 
                                    1
                                FROM CPR_CAMPANHA (NOLOCK)
                                WHERE
                                     LOWER(NOME) LIKE '%' + LOWER(@nome) + '%'
                                     AND GETDATE() BETWEEN  AND DT_INICIO AND DT_FIM
                                     AND ATIVO  = 1";

            var p = new { nome = new DbString() { Value = nome, Length = 100, IsAnsi = true } };
            return this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, p);
        }

        public Task<bool> IsCurrentAsync(int campanhaId)
        {
            const string sql = @"SELECT 
                                    1
                                FROM CPR_CAMPANHA (NOLOCK)
                                WHERE
                                     CPR_CAMPANHA_ID = @campanhaId
                                     AND GETDATE() BETWEEN  AND DT_INICIO AND DT_FIM
                                     AND ATIVO  = 1";

            return this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaId });
        }


        public async Task<Campanha> GetCurrentAsync(string nome)
        {
            const string sql = @"SELECT 
                                    *
                                FROM CPR_CAMPANHA (NOLOCK)
                                WHERE
                                     LOWER(NOME) = LOWER(@nome)
                                     AND GETDATE() BETWEEN DT_INICIO AND DT_FIM
                                     AND ATIVO  = 1";

            var p = new { nome = new DbString() { Value = nome, Length = 100, IsAnsi = true } };
            return await this.Connection.SqlConnection.QueryFirstOrDefaultAsync<Campanha>(sql, p);
        }

        public Task<Campanha> GetCurrentAsync(int campanhaId)
        {
            const string sql = @"SELECT 
                                    *
                                FROM CPR_CAMPANHA (NOLOCK)
                                WHERE
                                     CPR_CAMPANHA_ID = @campanhaId
                                     AND GETDATE() BETWEEN DT_INICIO AND DT_FIM
                                     AND ATIVO  = 1";

            return Connection.SqlConnection.QueryFirstOrDefaultAsync<Campanha>(sql, new { campanhaId });
        }

        public async Task<IEnumerable<Campanha>> GetAllAsync()
        {
            const string sql = @"SELECT 
                                    *
                                FROM CPR_CAMPANHA (NOLOCK) A
                                LEFT JOIN DBS_PARCEIRO B ON A.USUARIO_ID_PARCEIRO = B.USUARIO_ID_PARCEIRO
                                ORDER BY A.DT_FIM DESC, A.DT_INICIO DESC, A.NOME ASC;";

            return await this.Connection.SqlConnection.QueryAsync<Campanha, Parceiro, Campanha>(sql, (campanha, parceiro) =>
            {
                campanha.Parceiro = parceiro;
                return campanha;
            }, splitOn: "CPR_CAMPANHA_ID,USUARIO_ID_PARCEIRO");
        }

      

        public async Task<IEnumerable<Campanha>> GetAllActiveAsync()
        {
            const string sql = @"SELECT 
                                    *
                                FROM CPR_CAMPANHA (NOLOCK);";

            return await this.Connection.SqlConnection.QueryAsync<Campanha>(sql);
        }

        public async Task<Campanha> GetByIdAsync(int id)
        {
            const string sql = @"SELECT 
                                    *
                                FROM CPR_CAMPANHA (NOLOCK) A
                                LEFT JOIN DBS_PARCEIRO B ON A.USUARIO_ID_PARCEIRO = B.USUARIO_ID_PARCEIRO
                                WHERE A.CPR_CAMPANHA_ID = @id;";

            return (await this.Connection.SqlConnection.QueryAsync<Campanha, Parceiro, Campanha>(sql, (campanha, parceiro) =>
            {
                campanha.Parceiro = parceiro;
                return campanha;
            }, splitOn: "CPR_CAMPANHA_ID,USUARIO_ID_PARCEIRO", param: new { id })).FirstOrDefault();
        }

        public async Task<Campanha> GetByIdIncludeFasesAsync(int id)
        {
            var model = await GetByIdAsync(id);

            if (model == null)
            {
                return model;
            }

            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                     C.CPR_CAMPANHA_ID = @id; ";

            model.Fases = await this.Connection.SqlConnection.QueryAsync<CampanhaFase>(sql, new { id });

            return model;
        }

        public async Task<Campanha> GetByIdIncludeFasesAndPrevisaoFaturaAsync(int id)
        {
            var model = await GetByIdIncludeFasesAsync(id);

            if (model == null)
            {
                return model;
            }

            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_PREVISAO_FATURA  (NOLOCK) A
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON A.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                     C.CPR_CAMPANHA_ID = @id; ";

            model.PrevisaoFaturas = await this.Connection.SqlConnection.QueryAsync<CampanhaPrevisaoFatura>(sql, new { id });

            return model;
        }

        public async Task<bool> UpdateAsync(Campanha entity)
        {
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<int> GetTotalUsersAsync(int campanhaId)
        {
            try
            {
                const string sql = @"SELECT 
	                                COUNT(A.CPR_USUARIO_ID)
                                FROM CPR_USUARIO (NOLOCK) A
                                INNER JOIN CPR_CAMPANHA (NOLOCK) B ON A.CPR_CAMPANHA_ID = B.CPR_CAMPANHA_ID
                                WHERE A.CPR_CAMPANHA_ID = @campanhaId;";

                return await this.Connection.SqlConnection.ExecuteScalarAsync<int>(sql, new { campanhaId }, commandTimeout: 0);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

        public async Task<int> GetTotalPricesAsync(int campanhaId)
        {
            try
            {
                const string sql = @"SELECT 
	                                COUNT(A.CPR_USUARIO_ID)
                                FROM CPR_USUARIO (NOLOCK) A
                                INNER JOIN CPR_CAMPANHA (NOLOCK) B ON A.CPR_CAMPANHA_ID = B.CPR_CAMPANHA_ID
                                INNER JOIN CPR_USUARIO_PREMIO (NOLOCK) C ON A.CPR_USUARIO_ID = C.CPR_USUARIO_ID
                                WHERE A.CPR_CAMPANHA_ID = @campanhaId;";

                return await this.Connection.SqlConnection.ExecuteScalarAsync<int>(sql, new { campanhaId }, commandTimeout: 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

        public async Task<bool> RangeDateConflictRedemptionDateAsync(int campanhaId, DateTime dataInicio, DateTime dataFim)
        {
            const string sql = @"SELECT 
	                                1 
                                FROM CPR_CAMPANHA (NOLOCK) A
	                                INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) B ON B.CPR_CAMPANHA_ID = A.CPR_CAMPANHA_ID
                                WHERE
	                                 A.CPR_CAMPANHA_ID = @campanhaId
	                                 	 AND (@dataInicio > B.DATA_INICIO_RESGATE OR @dataInicio > B.DATA_FIM_RESGATE)
	                                     OR (@dataFim < B.DATA_INICIO_RESGATE OR @dataFim < B.DATA_FIM_RESGATE)";

            return await this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaId, dataInicio, dataFim });
        }
    }
}
