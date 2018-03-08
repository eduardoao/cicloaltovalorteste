using System.Collections.Generic;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Model.ValueObject;

namespace CicloVidaAltoValor.Application.Interfaces.Services
{
    public interface IFileProductService
    {
        Task ProcessAsync(IEnumerable<ProdutoArquivo> cast, Arquivo file, CampanhaFase campaignSeuDesejo);
        Task ProcessAsync(IEnumerable<ProdutoArquivo> enumerable, Arquivo file, Campanha campanhaFaseAtual);
    }
}
