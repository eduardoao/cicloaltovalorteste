using System;
using Dharma.Repository;

namespace CicloVidaAltoValor.Application.Model.Entities
{
    public class UsuarioAcesso : IEntity
    {
        public int UsuarioAcessoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
