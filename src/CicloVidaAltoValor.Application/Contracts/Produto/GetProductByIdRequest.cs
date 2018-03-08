using Microsoft.AspNetCore.Mvc;

namespace CicloVidaAltoValor.Application.Contracts.Produto
{
    public class GetProductByIdRequest
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
