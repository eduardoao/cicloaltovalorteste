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
    public class UsuarioComplementoRepository : SQLRepository<UsuarioComplemento>, IUsuarioComplementoRepository
    {
        public UsuarioComplementoRepository(DotzAppContext context) : base(context)
        {
        }

        public async Task<bool> UpdateAsync(UsuarioComplemento entity)
        {
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<UsuarioComplemento> GetByUserIdAndTypeComplementAsync(int usuarioId, TipoComplemento tipoComplemento)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_USUARIO_COMPLEMENTO  (NOLOCK) UC
                                INNER JOIN CPR_USUARIO (NOLOCK) U ON UC.CPR_USUARIO_ID = U.CPR_USUARIO_ID
                                WHERE UC.CPR_USUARIO_ID = @usuarioId AND UC.TIPO_COMPLEMENTO_ID = @tipoComplemento;";

            return (await this.Connection.SqlConnection.QueryAsync<UsuarioComplemento, Usuario, UsuarioComplemento>(sql, map:
                (usuarioComplemento, usuario) =>
                {
                    usuarioComplemento.Usuario = usuario;
                    return usuarioComplemento;
                }, splitOn: "CPR_USUARIO_ID,CPR_USUARIO_ID", param: new { usuarioId, tipoComplemento = tipoComplemento.GetHashCode() })).FirstOrDefault();
        }


        public async Task<IEnumerable<UsuarioComplemento>> GetAllByUserIdAndTypeComplementAsync(int usuarioId, TipoComplemento tipoComplemento)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_USUARIO_COMPLEMENTO  (NOLOCK) UC
                                INNER JOIN CPR_USUARIO (NOLOCK) U ON UC.CPR_USUARIO_ID = U.CPR_USUARIO_ID
                                WHERE UC.CPR_USUARIO_ID = @usuarioId AND UC.TIPO_COMPLEMENTO_ID = @tipoComplemento
                                ORDER BY UC.VALOR";

            return (await this.Connection.SqlConnection.QueryAsync<UsuarioComplemento, Usuario, UsuarioComplemento>(sql, map:
                (usuarioComplemento, usuario) =>
                {
                    usuarioComplemento.Usuario = usuario;
                    return usuarioComplemento;
                }, splitOn: "CPR_USUARIO_ID,CPR_USUARIO_ID", param: new { usuarioId, tipoComplemento = tipoComplemento.GetHashCode() }));
        }

        public async Task<IEnumerable<UsuarioComplemento>> GetAllByUserIdAndTypeComplementAndNameAsync(int usuarioId, TipoComplemento tipoComplemento, string nome)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_USUARIO_COMPLEMENTO  (NOLOCK) UC
                                INNER JOIN CPR_USUARIO (NOLOCK) U ON UC.CPR_USUARIO_ID = U.CPR_USUARIO_ID
                                WHERE UC.CPR_USUARIO_ID = @usuarioId AND UC.TIPO_COMPLEMENTO_ID = @tipoComplemento AND UPPER(UC.NOME) = UPPER(@nome)
                                ORDER BY UC.VALOR";

            var p = new { usuarioId, tipoComplemento = tipoComplemento.GetHashCode(), nome = new DbString() { Value = nome, Length = 180, IsAnsi = true } };
            return (await this.Connection.SqlConnection.QueryAsync<UsuarioComplemento, Usuario, UsuarioComplemento>(sql, map:
                (usuarioComplemento, usuario) =>
                {
                    usuarioComplemento.Usuario = usuario;
                    return usuarioComplemento;
                }, splitOn: "CPR_USUARIO_ID,CPR_USUARIO_ID", param: p));
        }

        public async Task<UsuarioComplemento> GetByUserIdAndTypeComplementAndNameAsync(int usuarioId, TipoComplemento tipoComplemento, string nome)
        {
            const string sql = @"SELECT  
                                        *
                                FROM CPR_USUARIO_COMPLEMENTO  (NOLOCK) UC
                                INNER JOIN CPR_USUARIO (NOLOCK) U ON UC.CPR_USUARIO_ID = U.CPR_USUARIO_ID
                                WHERE UC.CPR_USUARIO_ID = @usuarioId AND UC.TIPO_COMPLEMENTO_ID = @tipoComplemento AND UPPER(UC.NOME) = UPPER(@nome);";

            var p = new { usuarioId, tipoComplemento = tipoComplemento.GetHashCode(), nome = new DbString() { Value = nome, Length = 180, IsAnsi = true } };

            return (await this.Connection.SqlConnection.QueryAsync<UsuarioComplemento, Usuario, UsuarioComplemento>(sql, map:
                (usuarioComplemento, usuario) =>
                {
                    usuarioComplemento.Usuario = usuario;
                    return usuarioComplemento;
                }, splitOn: "CPR_USUARIO_ID,CPR_USUARIO_ID", param: p)).FirstOrDefault();
        }

        public async Task<UsuarioComplemento> InsertAsync(UsuarioComplemento entity)
        {
            entity.DataCriacao = DateTime.Now;
            await Connection.SqlConnection.InsertAsync(entity);
            return entity;
        }
    }
}
