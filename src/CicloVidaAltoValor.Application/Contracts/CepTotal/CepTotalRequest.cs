using Microsoft.AspNetCore.Mvc;

namespace CicloVidaAltoValor.Application.Contracts.CepTotal
{
    public class CepTotalRequest
    {
        [FromRoute]
        public string Cep { get; set; }
    }
}
