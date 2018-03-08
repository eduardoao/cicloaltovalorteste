using System.Collections.Generic;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Model.ValueObject;

namespace CicloVidaAltoValor.Application.Interfaces.Services
{
    public interface IFileUserStatusService
    {
        Task ProcessAsync(IEnumerable<UsuarioStatusArquivo> usersStatus, Arquivo file,
            CampanhaFase campaignSeuDesejo);


        Task<int> ProcessBulkCopyAsync(Arquivo file, CampanhaFase campaignSeuDesejo);
        Task<int> ProcessBulkCopyAsync(Arquivo file, Campanha campanha);
    }
}
