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
    public class CampanhaUsuarioAtualizacaoCadastroRepository : SQLRepository<CampanhaUsuarioAtualizacaoCadastro>, ICampanhaUsuarioAtualizacaoCadastroRepository
    {
        public CampanhaUsuarioAtualizacaoCadastroRepository(DotzAppContext context) : base(context)
        {
        }

        public async Task<bool> UpdateAsync(CampanhaUsuarioAtualizacaoCadastro entity)
        {
            entity.DataAtualizacao = DateTime.Now;
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<CampanhaUsuarioAtualizacaoCadastro> InsertAsync(CampanhaUsuarioAtualizacaoCadastro entity)
        {
            entity.DataCriacao = DateTime.Now;
            await Connection.SqlConnection.InsertAsync(entity);
            return entity;
        }

        public Task<bool> ExistAsync(int campanhaId, int usuarioId)
        {
            const string sql = @"SELECT 
                                    1 
                                FROM CPR_CAMPANHA_USUARIO_ATUALIZACAO_CADASTRO (NOLOCK)
                                WHERE
                                     CPR_CAMPANHA_ID = @campanhaId
                                     AND CPR_USUARIO_ID = @usuarioId;";

            return this.Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { campanhaId, usuarioId });
        }

        public async Task<IEnumerable<CampanhaUsuarioAtualizacaoCadastro>> GetAllNotBonusAsync(int campanhaId)
        {
            const string sql = @"SELECT 
	                                    * 
                                 FROM CPR_CAMPANHA_USUARIO_ATUALIZACAO_CADASTRO (NOLOCK) CA
	                             INNER JOIN CPR_USUARIO U (NOLOCK) ON CA.CPR_USUARIO_ID = U.CPR_USUARIO_ID
	                             WHERE 
                                      CA.BONIFICADO = 0 AND CA.CPR_CAMPANHA_ID = @campanhaId;";

            return await this.Connection.SqlConnection.QueryAsync<CampanhaUsuarioAtualizacaoCadastro, Usuario, CampanhaUsuarioAtualizacaoCadastro>(sql, map:
                (cadastro, usuario) =>
                {
                    cadastro.Usuario = usuario;
                    return cadastro;
                }, splitOn: "CPR_CAMPANHA_ID,CPR_USUARIO_ID", param: new { campanhaId });
        }
    }
}
