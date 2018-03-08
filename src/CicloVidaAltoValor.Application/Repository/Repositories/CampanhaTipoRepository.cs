using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;
using Dharma.Repository.SQL;

namespace CicloVidaAltoValor.Application.Repository.Repositories
{
    public class CampanhaTipoRepository : SQLRepository<CampanhaTipo> , ICampanhaTipoRepository
    {
        public CampanhaTipoRepository(DotzAppContext context) : base(context)
        {
        }
    }
}
