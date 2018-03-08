using System.Threading.Tasks;
using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IArquivoRepository : ISQLRepository<Arquivo>
    {

        Task<bool> UpdateAsync(Arquivo entity);
        Task<Arquivo> InsertAsync(Arquivo entity);
    }
}
