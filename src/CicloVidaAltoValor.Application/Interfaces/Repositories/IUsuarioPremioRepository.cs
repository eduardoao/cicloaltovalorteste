using System;
using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;
using System.Collections.Generic;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IUsuarioPremioRepository : ISQLRepository<UsuarioPremio>
    {
        Task<bool> UpdateAsync(UsuarioPremio entity);

        Task<UsuarioPremio> InsertAsync(UsuarioPremio entity);

        Task<IEnumerable<UsuarioPremio>> GetRedemptions(int campanhaId, DateTime? dataInicio, DateTime? dataFim);
        Task<bool> HasPrizeAsync(int userId);
    }
}
