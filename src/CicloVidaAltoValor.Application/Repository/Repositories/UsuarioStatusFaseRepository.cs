using System;
using System.Data.SqlClient;
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
    public class UsuarioStatusFaseRepository : SQLRepository<UsuarioStatusFase>, IUsuarioStatusFaseRepository
    {
        public UsuarioStatusFaseRepository(DotzAppContext context) : base(context)
        {
        }

        public async Task<bool> UpdateAsync(UsuarioStatusFase entity)
        {
            entity.DataAtualizacao = DateTime.Now;
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<UsuarioStatusFase> InsertAsync(UsuarioStatusFase entity)
        {
            entity.DataCriacao = DateTime.Now;

            await Connection.SqlConnection.InsertAsync(entity);

            return entity;
        }

        public Task<UsuarioStatusFase> GetByUserIdAsync(int userId)
        {
            const string sql = @"SELECT * FROM CPR_USUARIO_STATUS_FASE (NOLOCK) WHERE CPR_USUARIO_ID = @userId";

            return Connection.SqlConnection.QueryFirstOrDefaultAsync<UsuarioStatusFase>(sql, new { userId });
        }

        public async Task<UsuarioStatusFase> GetByUserIdAndCampaignActiveAsync(int userId, int campaignId)
        {
            const string sql = @"SELECT 
                                        * 
                                FROM CPR_USUARIO_STATUS_FASE (NOLOCK) US
                                INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) CF ON US.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                INNER JOIN CPR_CAMPANHA C (NOLOCK) ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                    C.CPR_CAMPANHA_ID = @campaignId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
	                                AND MONTH(CF.PERIODO) = MONTH(GETDATE()) AND YEAR(CF.PERIODO) = YEAR(GETDATE())
	                                AND C.ATIVO  = 1
                                    AND US.CPR_USUARIO_ID = @userId 
                                    AND US.ATIVO = 1;";

            return (await Connection.SqlConnection.QueryAsync<UsuarioStatusFase, CampanhaFase, Campanha, UsuarioStatusFase>(sql, map:
                (usuarioStatusFase, campanhaFase, campanha) =>
                {
                    campanhaFase.Campanha = campanha;
                    usuarioStatusFase.CampanhaFase = campanhaFase;
                    return usuarioStatusFase;

                },
                splitOn: "CPR_ARQUIVO_ID,CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID",
                param: new { userId, campaignId })).FirstOrDefault();

        }

        public async Task<UsuarioStatusFase> GetByUserIdAndCampaignAndCampaignFaseActiveAsync(int userId, int campaignId, int campaignFaseId)
        {
            const string sql = @"SELECT 
                                        * 
                                FROM CPR_USUARIO_STATUS_FASE (NOLOCK) US
                                INNER JOIN CPR_CAMPANHA_FASE (NOLOCK) CF ON US.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                INNER JOIN CPR_CAMPANHA C (NOLOCK) ON CF.CPR_CAMPANHA_ID = C.CPR_CAMPANHA_ID
                                WHERE 
                                    US.CPR_CAMPANHA_FASE_ID = @campaignFaseId
                                    AND C.CPR_CAMPANHA_ID = @campaignId
	                                AND GETDATE() BETWEEN C.DT_INICIO AND C.DT_FIM
	                                AND C.ATIVO  = 1
                                    AND US.CPR_USUARIO_ID = @userId 
                                    AND US.ATIVO = 1;";

            return (await Connection.SqlConnection.QueryAsync<UsuarioStatusFase, CampanhaFase, Campanha, UsuarioStatusFase>(sql, map:
                (usuarioStatusFase, campanhaFase, campanha) =>
                {
                    campanhaFase.Campanha = campanha;
                    usuarioStatusFase.CampanhaFase = campanhaFase;
                    return usuarioStatusFase;

                },
                splitOn: "CPR_ARQUIVO_ID,CPR_CAMPANHA_FASE_ID,CPR_CAMPANHA_ID",
                param: new { userId, campaignId, campaignFaseId })).FirstOrDefault();

        }

        public Task<UsuarioStatusFase> GetByCampaignFaseIdAndUserIdActiveAsync(int campaignFaseId, int userId)
        {
            const string sql = @"SELECT * FROM CPR_USUARIO_STATUS_FASE (NOLOCK) WHERE CPR_CAMPANHA_FASE_ID = @campaignFaseId AND CPR_USUARIO_ID = @userId AND ATIVO = 1;";

            return Connection.SqlConnection.QueryFirstOrDefaultAsync<UsuarioStatusFase>(sql, new { campaignFaseId, userId });

        }

        public async Task FixUserStatusAsync(int campanhaId)
        {

            using (var conn = new SqlConnection(this.Connection.SqlConnection.ConnectionString))
            {
                var comm = conn.CreateCommand();

                comm.CommandTimeout = 0;
                conn.Open();
                
                // acertando o campo cpr_usuario_id
                comm.CommandText =
                    "UPDATE A SET A.CPR_USUARIO_ID = B.CPR_USUARIO_ID FROM BB_TEMP_USUARIO_STATUS A, CPR_USUARIO B WHERE A.CPR_USUARIO_CPF  = B.DOCUMENTO AND B.CPR_CAMPANHA_ID = " + campanhaId;
                await comm.ExecuteNonQueryAsync();

                comm.CommandTimeout = 0;
                // acertando o campo cpr_campanha_fase_id
                comm.CommandText =
                    "UPDATE A SET A.CPR_CAMPANHA_FASE_ID = B.CPR_CAMPANHA_FASE_ID FROM BB_TEMP_USUARIO_STATUS A, CPR_CAMPANHA_FASE B WHERE MONTH(B.PERIODO) = MONTH(A.PERIODO) AND YEAR(B.PERIODO)= YEAR(A.PERIODO) AND B.CPR_CAMPANHA_ID = " + campanhaId;
                await comm.ExecuteNonQueryAsync();

                comm.CommandTimeout = 0;
                // desativando os registros caso existam
                comm.CommandText = @"MERGE CPR_USUARIO_STATUS_FASE AS T
                                     USING BB_TEMP_USUARIO_STATUS AS S ON (T.CPR_USUARIO_ID = S.CPR_USUARIO_ID AND T.CPR_CAMPANHA_FASE_ID = S.CPR_CAMPANHA_FASE_ID)
                                     WHEN MATCHED THEN UPDATE SET T.ATIVO = 0, T.DATA_ATUALIZACAO = GetDate();";
                await comm.ExecuteNonQueryAsync();

                comm.CommandTimeout = 0;
                // incluindo os registros
                comm.CommandText = @"MERGE CPR_USUARIO_STATUS_FASE AS T
                                     USING BB_TEMP_USUARIO_STATUS AS S ON (T.CPR_USUARIO_ID = S.CPR_USUARIO_ID AND T.CPR_CAMPANHA_FASE_ID = S.CPR_CAMPANHA_FASE_ID AND T.ATIVO = 1)
                                    WHEN NOT MATCHED BY TARGET AND CPR_USUARIO_ID IS NOT NULL
                                        THEN INSERT (
                                        CPR_ARQUIVO_ID
                                        ,CPR_USUARIO_ID
                                        ,CPR_CAMPANHA_FASE_ID
                                        ,PERIODO
                                        ,META
                                        ,FAIXA_META
                                        ,GASTO
                                        ,GASTO_PERCENTUAL
                                        ,DESAFIO_1
                                        ,DESAFIO_2
                                        ,DESAFIO_3
                                        ,DESAFIO_4
                                        ,DESAFIO_5
                                        ,DESAFIO_6
                                        ,DESAFIO_7
                                        ,CATALOGO
                                        ,ATIVO
                                        ,DATA_ATUALIZACAO
                                        ,DATA_CRIACAO) 
                                        VALUES 
                                        (S.CPR_ARQUIVO_ID
                                        ,S.CPR_USUARIO_ID
                                        ,S.CPR_CAMPANHA_FASE_ID
                                        ,S.PERIODO
                                        ,S.META
                                        ,S.FAIXA_META
                                        ,S.GASTO
                                        ,S.GASTO_PERCENTUAL
                                        ,S.DESAFIO_1
                                        ,S.DESAFIO_2
                                        ,S.DESAFIO_3
                                        ,S.DESAFIO_4
                                        ,S.DESAFIO_5
                                        ,S.DESAFIO_6
                                        ,S.DESAFIO_7
                                        ,S.CATALOGO
                                        ,1
                                        ,NULL
                                        ,GetDate());";

                await comm.ExecuteNonQueryAsync();


            }
        }
    }
}
