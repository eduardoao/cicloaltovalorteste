using AutoMapper;
using CicloVidaAltoValor.Application.Contracts.Usuario;
using CicloVidaAltoValor.Application.Contracts.UsuarioStatusFase;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.ProfileMappings
{
    public class UsuarioStatusFaseProfile : Profile
    {
        public UsuarioStatusFaseProfile()
        {

            CreateMap<UsuarioStatusFase, UsuarioStatusFaseViewModel>()
                 .ForMember(x => x.Catalogo, d => d.MapFrom(c => c.Catalogo))
                 //.ForMember(x => x.Id, d => d.MapFrom(c => c.Id))
                 .ForMember(x => x.ArquivoId, d => d.MapFrom(c => c.ArquivoId))
                 .ForMember(x => x.DataCriacao, d => d.MapFrom(c => c.DataCriacao))
                 .ForMember(x => x.FaixaMeta, d => d.MapFrom(c => c.FaixaMeta))
                 .ForMember(x => x.Gasto, d => d.MapFrom(c => c.Gasto))
                 .ForMember(x => x.GastoPercentual, d => d.MapFrom(c => c.GastoPercentual))
                 .ForMember(x => x.Usuario, d => d.MapFrom(c => Mapper.Map<UsuarioViewModel>(c.Usuario)))
                 .ForMember(x => x.Meta, d => d.MapFrom(c => c.Meta))
                 .ForMember(x => x.Periodo, d => d.MapFrom(c => c.Periodo))
                 .ForMember(x => x.Desafio1, d => d.MapFrom(c => c.Desafio1))
                 .ForMember(x => x.Desafio2, d => d.MapFrom(c => c.Desafio2))
                 .ForMember(x => x.Desafio3, d => d.MapFrom(c => c.Desafio3))
                 .ForMember(x => x.Desafio4, d => d.MapFrom(c => c.Desafio4))
                 .ForMember(x => x.Desafio5, d => d.MapFrom(c => c.Desafio5))
                 .ForMember(x => x.Desafio6, d => d.MapFrom(c => c.Desafio6))
                 .ForMember(x => x.Desafio7, d => d.MapFrom(c => c.Desafio7))
                 .ForMember(x => x.Ativo, d => d.MapFrom(c => c.Ativo));

        }
    }
}
