using System.Collections.Generic;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Model.ValueObject;

namespace CicloVidaAltoValor.Application.Interfaces.Services
{
    public interface IFileUserService
    {
        Task ProcessAsync(IEnumerable<UsuarioArquivo> users, Arquivo file,  CampanhaFase campaignSeuDesejo);
        Task<int> ProcessBulkCopyAsync(Arquivo file, CampanhaFase campanhaFaseAtual);
        Task<int> ProcessBulkCopyAsync(Arquivo file, Campanha campanhaFaseAtual);
    }
}
