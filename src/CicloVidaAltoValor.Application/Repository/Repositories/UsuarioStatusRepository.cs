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
    public class UsuarioStatusRepository : SQLRepository<UsuarioStatus>, IUsuarioStatusRepository
    {
        public UsuarioStatusRepository(DotzAppContext context) : base(context)
        {
        }

        public Task FixUserStatusAsync(int campanhaId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanPrizeAsync(int userId)
        {
            const string sql = @"SELECT 
                                       * 
                                 FROM CPR_USUARIO_STATUS (NOLOCK) WHERE CPR_USUARIO_ID = @userId AND HABILITAR_TROCAR = 1";

            return await Connection.SqlConnection.ExecuteScalarAsync<bool>(sql, new { userId });
        }

        public Task<UsuarioStatus> GetByUserIdStatusActiveAsync(int userId)
        {
            const string sql = @"SELECT * FROM CPR_USUARIO_STATUS (NOLOCK) WHERE CPR_USUARIO_ID = @userId";
            return Connection.SqlConnection.QueryFirstOrDefaultAsync<UsuarioStatus>(sql, new { userId });

        }

        public async Task<UsuarioStatus> InsertAsync(UsuarioStatus entity)
        {
            entity.DataCriacao = DateTime.Now;
            entity.Id =  Convert.ToInt32(await Connection.SqlConnection.InsertAsync(entity));

            return entity;
        }
    }
}
