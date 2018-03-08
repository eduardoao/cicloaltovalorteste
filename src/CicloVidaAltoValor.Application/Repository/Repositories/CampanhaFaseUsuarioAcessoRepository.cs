using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dharma.Repository.SQL;
using Dommel;
using CicloVidaAltoValor.Application.Contracts.Campanha;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class CampanhaFaseUsuarioAcessoRepository : SQLRepository<CampanhaFaseUsuarioAcesso>, ICampanhaFaseUsuarioAcessoRepository
    {
        public CampanhaFaseUsuarioAcessoRepository(DotzAppContext context) : base(context)
        {
        }


        public async Task<bool> UpdateAsync(CampanhaFaseUsuarioAcesso entity)
        {
            entity.DataAtualizacao = DateTime.Now;
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<bool> UpdateBonusByUserAsync(int usuarioId)
        {
            const string sql = @"UPDATE [CPR_CAMPANHA_FASE_USUARIO_ACESSO] SET BONIFICADO = 1, DATA_ATUALIZACAO = GETDATE()  WHERE CPR_USUARIO_ID = @usuarioId";

            return (await this.Connection.SqlConnection.ExecuteAsync(sql, new { usuarioId })) != 0;
        }

       


        public async Task<dynamic> GetAllAccessUsersAndFaseAsync(int campanhaId)
        {
            const string sql = @"SELECT 
	                                COUNT(CFA.CPR_USUARIO_ID) [QUANTIDADE], CFA.CPR_USUARIO_ID, U.DOCUMENTO, CFA.CPR_CAMPANHA_FASE_ID --,CF.PERIODO, CF.FASE
                                FROM [CPR_CAMPANHA_FASE_USUARIO_ACESSO] CFA (NOLOCK)
                                    INNER JOIN CPR_USUARIO U (NOLOCK) ON CFA.CPR_USUARIO_ID = U.CPR_USUARIO_ID
                                    INNER JOIN CPR_CAMPANHA_FASE CF (NOLOCK) ON CFA.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                WHERE U.CPR_CAMPANHA_ID = @campanhaId
                                GROUP BY CFA.CPR_USUARIO_ID, CFA.CPR_CAMPANHA_FASE_ID,U.DOCUMENTO --,CF.PERIODO, CF.FASE";

            return await this.Connection.SqlConnection.QueryAsync<dynamic>(sql, new { campanhaId });
        }

        public Task<bool> ExistAsync(int campanhaFaseId, int usuarioId)
        {

            const string sql = @"SELECT 
                                    1
                                FROM CPR_CAMPANHA_FASE_USUARIO_ACESSO (NOLOCK)
                                WHERE
                                     CPR_CAMPANHA_FASE_ID = @campanhaFaseId
                                     AND CPR_USUARIO_ID = @usuarioId;";

            return this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaFaseId, usuarioId });
        }

        public Task<CampanhaFaseUsuarioAcesso> GetByCampaignFaseAndUserIdAsync(int campanhaFaseId, int usuarioId)
        {
            const string sql = @"SELECT 
                                    *
                                FROM CPR_CAMPANHA_FASE_USUARIO_ACESSO (NOLOCK)
                                WHERE
                                     CPR_CAMPANHA_FASE_ID = @campanhaFaseId
                                     AND CPR_USUARIO_ID = @usuarioId;";

            return this.Connection.SqlConnection.QueryFirstOrDefaultAsync<CampanhaFaseUsuarioAcesso>(sql, new { campanhaFaseId, usuarioId });
        }

        public async Task<IEnumerable<CampanhaFaseUsuarioAcesso>> GetAllNotBonusAsync(int campanhaFaseId)
        {

            const string sql = @"SELECT 
	                                    * 
                                 FROM CPR_CAMPANHA_FASE_USUARIO_ACESSO (NOLOCK) CA
	                             INNER JOIN CPR_USUARIO U (NOLOCK) ON CA.CPR_USUARIO_ID = U.CPR_USUARIO_ID
	                             WHERE 
                                      CA.BONIFICADO = 0 AND CA.CPR_CAMPANHA_FASE_ID = @campanhaFaseId;";

            return await this.Connection.SqlConnection.QueryAsync<CampanhaFaseUsuarioAcesso, Usuario, CampanhaFaseUsuarioAcesso>(sql, map:
                (campanhaUsuarioAcesso, usuario) =>
                {
                    campanhaUsuarioAcesso.Usuario = usuario;
                    return campanhaUsuarioAcesso;
                }, splitOn: "CPR_CAMPANHA_FASE_ID,CPR_USUARIO_ID", param: new { campanhaFaseId });
        }

        public async Task<IEnumerable<Usuario>> GetAllNotBonusAccessFaseThreeTimesAsync(int campanhaId)
        {
            const string sql = @";WITH CTE 
                                    AS
                                    (
                                        SELECT 
                                            CFA.CPR_USUARIO_ID,U.DOCUMENTO, CF.FASE, COUNT(CFA.CPR_USUARIO_ID) [QUANTIDADE]
                                        FROM [CPR_CAMPANHA_FASE_USUARIO_ACESSO] CFA (NOLOCK)
                                        INNER JOIN CPR_USUARIO U (NOLOCK) ON CFA.CPR_USUARIO_ID = U.CPR_USUARIO_ID
                                        INNER JOIN CPR_CAMPANHA_FASE CF (NOLOCK) ON CFA.CPR_CAMPANHA_FASE_ID = CF.CPR_CAMPANHA_FASE_ID
                                        WHERE U.CPR_CAMPANHA_ID = @campanhaId 
                                              AND NOT EXISTS(SELECT TOP 1 1 
                                                             FROM [CPR_CAMPANHA_FASE_USUARIO_ACESSO] (NOLOCK) 
                                                             WHERE CPR_USUARIO_ID = CFA.CPR_USUARIO_ID 
                                                                   AND CPR_CAMPANHA_FASE_ID = CFA.CPR_CAMPANHA_FASE_ID AND BONIFICADO = 1)
                                        GROUP BY CFA.CPR_USUARIO_ID, CFA.CPR_CAMPANHA_FASE_ID,U.DOCUMENTO,CF.FASE
                                        HAVING COUNT(CFA.CPR_USUARIO_ID) >= 3 
                                    )
                                    SELECT 
                                          CPR_USUARIO_ID,DOCUMENTO 
                                    FROM CTE 
                                    GROUP BY CPR_USUARIO_ID,DOCUMENTO 
                                    HAVING COUNT(FASE) >= (SELECT COUNT(1) FROM CPR_CAMPANHA_FASE (NOLOCK) WHERE CPR_CAMPANHA_ID = @campanhaId) ;";

            return await this.Connection.SqlConnection.QueryAsync<Usuario>(sql, param: new { campanhaId });
        }


    }
}
