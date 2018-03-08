using Dharma.Repository.SQL;
using CicloVidaAltoValor.Application.Interfaces.Repositories;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Repository.Contexts;

namespace CicloVidaAltoValor.Application.Repository.Repositories 
{
    public class CampanhaPrevisaoFaturaRepository : SQLRepository<CampanhaPrevisaoFatura>, ICampanhaPrevisaoFaturaRepository 
    {
        public CampanhaPrevisaoFaturaRepository (DotzAppContext context) : base (context) 
        {

        }
    }

}