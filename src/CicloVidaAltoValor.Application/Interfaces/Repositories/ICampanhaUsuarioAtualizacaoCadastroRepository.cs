using System.Collections.Generic;
using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface ICampanhaUsuarioAtualizacaoCadastroRepository : ISQLRepository<CampanhaUsuarioAtualizacaoCadastro> 
    {
        Task<bool> UpdateAsync(CampanhaUsuarioAtualizacaoCadastro entity);
        Task<CampanhaUsuarioAtualizacaoCadastro> InsertAsync(CampanhaUsuarioAtualizacaoCadastro entity);
        Task<bool> ExistAsync(int campanhaId, int usuarioId);
        Task<IEnumerable<CampanhaUsuarioAtualizacaoCadastro>>  GetAllNotBonusAsync(int campanhaId);
    }
}
