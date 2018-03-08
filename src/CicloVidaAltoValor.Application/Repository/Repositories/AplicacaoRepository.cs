using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class AplicacaoRepository : SQLRepository<Aplicacao>, IAplicacaoRepository
    {
        public AplicacaoRepository(DotzSystemContext context) : base(context)
        {
        }
    }
}
