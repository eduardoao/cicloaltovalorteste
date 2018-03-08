using System;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;
using Dharma.Repository.SQL;
using Dommel;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class UsuarioAcessoRepository : SQLRepository<UsuarioAcesso>, IUsuarioAcessoRepository
    {
        public UsuarioAcessoRepository(DotzAppContext context) : base(context)
        {
        }

        public async Task<UsuarioAcesso> InsertAsync(UsuarioAcesso entity)
        {

            entity.UsuarioId = Convert.ToInt32(await Connection.SqlConnection.InsertAsync(entity, this.Connection.CurrentTransaction));

            return entity;
        }
    }
}
