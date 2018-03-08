using System;
using System.Threading.Tasks;
using Dharma.Repository.SQL;
using Dommel;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class ArquivoRepository : SQLRepository<Arquivo>, IArquivoRepository
    {
        public ArquivoRepository(DotzAppContext context) : base(context)
        {
        }

        public async Task<bool> UpdateAsync(Arquivo entity)
        {
            return await Connection.SqlConnection.UpdateAsync(entity);
        }

        public async Task<Arquivo> InsertAsync(Arquivo entity)
        {
            entity.DataCriacao = DateTime.Now;

            entity.Id = Convert.ToInt32(await Connection.SqlConnection.InsertAsync(entity));

            return entity;
        }
    }
}
