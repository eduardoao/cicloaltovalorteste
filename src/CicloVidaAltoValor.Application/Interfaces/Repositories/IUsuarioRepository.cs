using System;
using System.Threading.Tasks;
using CicloVidaAltoValor.Application.Model.Entities;
using Dharma.Repository.SQL;

namespace CicloVidaAltoValor.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository : ISQLRepository<Usuario>
    {
        Task<Usuario> GetByCampaignAndDocumentAsync(int campanhaId, string documento);
        Task<bool> UpdateAsync(Usuario entity);
        Task<Usuario> InsertAsync(Usuario entity);
        Task<Usuario> GetByBirthDateAndDocumentAndCampaign(int campanhaId, string cpf, DateTime dataNascimento);
        Task<bool> HasUpdateAddressRegister(int userId);

        Task<Usuario> GetByDocumentAndCampaign(int campanhaId, string documento);

        Task FixUserAndComplement();
    }
}
