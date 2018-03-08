using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dharma.Repository.SQL;
using Dommel;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class CampanhaFaseRepository : SQLRepository<CampanhaFase>, ICampanhaFaseRepository
    {
        public CampanhaFaseRepository(DotzAppContext context) : base(context)
        {
        }

        public async Task<bool> RedemptionAlreadyUseAsync(int campanhaId, int campanhaFaseId, DateTime dataInicialResgate, DateTime dataFinalResgate)
        {
            const string sql = @"SELECT  
                                      1
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                WHERE 
                                    CF.CPR_CAMPANHA_ID = @campanhaId 
                                    AND CF.CPR_CAMPANHA_FASE_ID <> @campanhaFaseId
	                                AND (@dataInicialResgate BETWEEN CF.DATA_INICIO_RESGATE AND CF.DATA_FIM_RESGATE 
                                           OR @dataFinalResgate BETWEEN CF.DATA_INICIO_RESGATE AND CF.DATA_FIM_RESGATE
                                        );";

            return await this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaId, campanhaFaseId, dataInicialResgate, dataFinalResgate });
        }

        public async Task<bool> UpdateAsync(CampanhaFase entity)
        {
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<CampanhaFase> GetByIdAsync(int id)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                LEFT JOIN DBS_PARCEIRO P ON C.USUARIO_ID_PARCEIRO = P.USUARIO_ID_PARCEIRO
                                WHERE 
                                    CF.CPR_CAMPANHA_FASE_ID = @id";

            return (await this.Connection.SqlConnection.QueryAsync<CampanhaFase, Campanha, Parceiro, CampanhaFase>(sql, map:
                (campanhaFase, campanha, parceiro) =>
                {
                    campanha.Parceiro = parceiro;
                    campanhaFase.Campanha = campanha;
                    return campanhaFase;
                }, splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID,USUARIO_ID_PARCEIRO", param: new { id })).FirstOrDefault();
        }

        public async Task<IEnumerable<CampanhaFase>> GetAllByCampaignIdAsync(int campanhaId)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                LEFT JOIN DBS_PARCEIRO P ON C.USUARIO_ID_PARCEIRO = P.USUARIO_ID_PARCEIRO
                                WHERE 
                                     C.CPR_CAMPANHA_ID = @campanhaId;
                                ";

            return await this.Connection.SqlConnection.QueryAsync<CampanhaFase, Campanha, Parceiro, CampanhaFase>(sql, map:
                (campanhaFase, campanha, parceiro) =>
                {
                    campanha.Parceiro = parceiro;
                    campanhaFase.Campanha = campanha;
                    return campanhaFase;
                }, splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID,USUARIO_ID_PARCEIRO", param: new { campanhaId });



        }

        public Task<bool> IsCurrentAsync(int campanhaId)
        {
            const string sql = @"SELECT 
                                    1 
                                FROM CPR_CAMPANHA_FASE (NOLOCK)
                                WHERE
                                     CPR_CAMPANHA_ID = @campanhaId 
                                     AND MONTH(PERIODO) = MONTH(GETDATE()) AND YEAR(PERIODO) = YEAR(GETDATE())";

            return this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaId });
        }

        public async Task<CampanhaFase> GetByPeriodAsync(DateTime period)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                    GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
	                                AND MONTH(CF.PERIODO) = MONTH(@period) AND YEAR(CF.PERIODO) = YEAR(@period)
	                                AND ATIVO  = 1";

            return (await this.Connection.SqlConnection.QueryAsync<CampanhaFase, Campanha, CampanhaFase>(sql, map:
                (campanhaFase, campanha) =>
                {
                    campanhaFase.Campanha = campanha;
                    return campanhaFase;
                }, splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID", param: new { period })).FirstOrDefault();
        }


        public async Task<CampanhaFase> GetCurrentAsync(int campanhaId)
        {

            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE C.CPR_CAMPANHA_ID = @campanhaId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
	                                AND MONTH(CF.PERIODO) = MONTH(GETDATE()) AND YEAR(CF.PERIODO) = YEAR(GETDATE())
	                                AND ATIVO  = 1";

            return (await this.Connection.SqlConnection.QueryAsync<CampanhaFase, Campanha, CampanhaFase>(sql, map:
                (campanhaFase, campanha) =>
                {
                    campanhaFase.Campanha = campanha;
                    return campanhaFase;
                }, splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID", param: new { campanhaId })).FirstOrDefault();
        }


        public async Task<CampanhaFase> GetCurrentRedemptionAsync(int campanhaId)
        {

            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE C.CPR_CAMPANHA_ID = @campanhaId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
	                                AND GETDATE() BETWEEN CF.DATA_INICIO_RESGATE AND CF.DATA_FIM_RESGATE
	                                AND ATIVO = 1";

            return (await this.Connection.SqlConnection.QueryAsync<CampanhaFase, Campanha, CampanhaFase>(sql, map:
                (campanhaFase, campanha) =>
                {
                    campanhaFase.Campanha = campanha;
                    return campanhaFase;
                }, splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID", param: new { campanhaId })).FirstOrDefault();
        }

        public async Task<CampanhaFase> GetCurrentAsync(string nome)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE LOWER(C.NOME) = LOWER(@nome)
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
	                                AND MONTH(CF.PERIODO) = MONTH(GETDATE()) AND YEAR(CF.PERIODO) = YEAR(GETDATE())
	                                AND ATIVO  = 1";

            var p = new { nome = new DbString() { Value = nome, Length = 100, IsAnsi = true } };
            return (await this.Connection.SqlConnection.QueryAsync<CampanhaFase, Campanha, CampanhaFase>(sql, map:
                (campanhaFase, campanha) =>
                {
                    campanhaFase.Campanha = campanha;
                    return campanhaFase;
                }, splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID", param: p)).FirstOrDefault();
        }

        public async Task<bool> PeriodAlreadyUseAsync(int campanhaId, int campanhaFaseId, DateTime periodo)
        {
            const string sql = @"SELECT  
                                      1
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                WHERE 
                                    CF.CPR_CAMPANHA_ID = @campanhaId 
                                    AND CF.CPR_CAMPANHA_FASE_ID <> @campanhaFaseId
	                                AND MONTH(CF.PERIODO) = MONTH(@periodo) AND YEAR(CF.PERIODO) = YEAR(@periodo);";

            return await this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaId, campanhaFaseId, periodo });

        }

        public async Task<bool> RedemptionRangeInCampaignRangeAsync(int campanhaId, int campanhaFaseId, DateTime dataInicioResgate, DateTime dataFimResgate)
        {
            const string sql = @"SELECT 
	                                1
                                FROM 
	                                CPR_CAMPANHA_FASE (NOLOCK) A 
	                                INNER JOIN CPR_CAMPANHA B ON A.CPR_CAMPANHA_ID = B.CPR_CAMPANHA_ID
                                WHERE
	                                    A.CPR_CAMPANHA_FASE_ID = @campanhaFaseId
                                        AND A.CPR_CAMPANHA_ID = @campanhaId 
	                                    AND (NOT @dataInicioResgate BETWEEN B.DT_INICIO AND B.DT_FIM)
	                                    OR (NOT @dataFimResgate BETWEEN B.DT_INICIO AND B.DT_FIM);";

            return await this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaId, campanhaFaseId, dataInicioResgate, dataFimResgate });
        }

        public async Task<CampanhaFase> GetLastFaseAsync(int campanhaId)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE C.CPR_CAMPANHA_ID = @campanhaId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
									AND CF.FASE IN (SELECT  MAX(FASE) FROM CPR_CAMPANHA_FASE WHERE CPR_CAMPANHA_ID = @campanhaId )
	                                AND ATIVO = 1";

            return (await this.Connection.SqlConnection.QueryAsync<CampanhaFase, Campanha, CampanhaFase>(sql, map:
                (campanhaFase, campanha) =>
                {
                    campanhaFase.Campanha = campanha;
                    return campanhaFase;
                }, splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID", param: new { campanhaId })).FirstOrDefault();
        }

        public async Task<CampanhaFase> GetLastFaseAsync(string nome)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE LOWER(C.NOME) = LOWER(@nome)
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
									AND CF.FASE IN (SELECT  MAX(FASE) FROM CPR_CAMPANHA_FASE WHERE LOWER(NOME) = LOWER(@nome) )
	                                AND ATIVO = 1";

            var p = new { nome = new DbString() { Value = nome, Length = 100, IsAnsi = true } };
            return (await this.Connection.SqlConnection.QueryAsync<CampanhaFase, Campanha, CampanhaFase>(sql, map:
                (campanhaFase, campanha) =>
                {
                    campanhaFase.Campanha = campanha;
                    return campanhaFase;
                }, splitOn: "CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID", param: p)).FirstOrDefault();
        }

        public async Task<bool> IsOverFase(int campanhaId)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_CAMPANHA_FASE  (NOLOCK) CF
                                INNER JOIN CPR_CAMPANHA (NOLOCK) C ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE C.CPR_CAMPANHA_ID = @campanhaId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
									AND CF.FASE IN (SELECT  MAX(FASE) FROM CPR_CAMPANHA_FASE WHERE CPR_CAMPANHA_ID = @campanhaId )
                                    AND CF.PERIODO >= (SELECT DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0))
	                                AND ATIVO = 1";

            return await this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { @campanhaId });
        }

        public async Task<CampanhaFase> GetCurrentSpentPeriodAsync()
        {
            const string sql = @"SELECT * 
                                FROM CPR_CAMPANHA_FASE (NOLOCK) CF
								WHERE 
								    MONTH(DATA_INICIO_RESGATE) = MONTH(GETDATE())
								AND YEAR(DATA_INICIO_RESGATE) = YEAR(GETDATE())
								AND GETDATE() < DATA_INICIO_RESGATE;";

            return await this.Connection.SqlConnection.QueryFirstOrDefaultAsync<CampanhaFase>(sql);
        }
    }
}
