using System.Collections.Generic;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Interfaces.Model;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<bool> Process();
        Task<bool> ReadFile(ICollection<IArquivo> contentsFile, Arquivo file);
    }
}
