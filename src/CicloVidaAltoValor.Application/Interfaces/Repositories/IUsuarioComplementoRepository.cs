using System.Collections.Generic;
using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Enum;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IUsuarioComplementoRepository : ISQLRepository<UsuarioComplemento>
    {
        Task<UsuarioComplemento> InsertAsync(UsuarioComplemento entity);

        Task<bool> UpdateAsync(UsuarioComplemento entity);

        Task<UsuarioComplemento> GetByUserIdAndTypeComplementAsync(int usuarioId, TipoComplemento tipoComplemento);

        Task<IEnumerable<UsuarioComplemento>> GetAllByUserIdAndTypeComplementAsync(int usuarioId, TipoComplemento tipoComplemento);
        
        Task<IEnumerable<UsuarioComplemento>> GetAllByUserIdAndTypeComplementAndNameAsync(int usuarioId, TipoComplemento tipoComplemento, string nome);

        Task<UsuarioComplemento> GetByUserIdAndTypeComplementAndNameAsync(int usuarioId, TipoComplemento tipoComplemento, string nome);

    }
}
