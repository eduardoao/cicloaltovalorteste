using System;
using AutoMapper;
using CicloVidaAltoValor.Application.Contracts.Usuario;
using CicloVidaAltoValor.Application.Extensions;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Model.ValueObject;

namespace CicloVidaAltoValor.Application.ProfileMappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
     
            CreateMap<UsuarioArquivo, Usuario>()
                .ForMember(x => x.Nome, d => d.MapFrom(c => c.Nome))
                .ForMember(x => x.Documento, d => d.MapFrom(c => c.Documento))
                .ForMember(x => x.DataNascimento, d => d.MapFrom(c => c.DataNascimento))
                .ForMember(x => x.Cidade, d => d.MapFrom(c => c.Cidade))
                .ForMember(x => x.Bairro, d => d.MapFrom(c => c.Bairro))
                .ForMember(x => x.Carteira, d => d.MapFrom(c => c.Carteira))
                .ForMember(x => x.Cep, d => d.MapFrom(c => c.Cep))
                .ForMember(x => x.Email, d => d.MapFrom(c => c.Email))
                .ForMember(x => x.Estado, d => d.MapFrom(c => c.Estado))
                .ForMember(x => x.Logradouro, d => d.MapFrom(c => c.Logradouro))
                .ForMember(x => x.NumeroLogradouro, d => d.MapFrom(c => c.NumeroLogradouro))
                .ForMember(x => x.Sexo, d => d.MapFrom(c => c.Sexo))
                .ForMember(x => x.Complemento, d => d.MapFrom(c => c.Complemento))
                .ForMember(x => x.DddResidencial, d => d.MapFrom(c => c.DddResidencial))
                .ForMember(x => x.TelefoneResidencial, d => d.MapFrom(c => c.TelefoneResidencial))
                .ForMember(x => x.DddCelular, d => d.MapFrom(c => c.DddCelular))
                .ForMember(x => x.TelefoneCelular, d => d.MapFrom(c => c.TelefoneCelular))
                .ForMember(x => x.DddComercial, d => d.MapFrom(c => c.DddComercial))
                .ForMember(x => x.TelefoneComercial, d => d.MapFrom(c => c.TelefoneComercial))
                .ForMember(x => x.DataOptin, d => d.UseValue(DateTime.Now));


            CreateMap<UsuarioComplementoArquivo, UsuarioComplemento>()
                .ForMember(x => x.Nome, d => d.MapFrom(c => c.Nome))
                .ForMember(x => x.Valor, d => d.MapFrom(c => c.Valor))
                .ForMember(x => x.TipoComplementoId, d => d.MapFrom(c => c.TipoComplementoId));




            CreateMap<UsuarioViewModel, Usuario>()
                .ForMember(x => x.Nome, d => d.MapFrom(c => c.Nome))
                .ForMember(x => x.Documento, d => d.MapFrom(c => c.Documento.JustNumbers()))
                .ForMember(x => x.DataNascimento, d => d.MapFrom(c => c.DataNascimento))
                .ForMember(x => x.Cidade, d => d.MapFrom(c => c.Cidade))
                .ForMember(x => x.Bairro, d => d.MapFrom(c => c.Bairro))
                .ForMember(x => x.Cep, d => d.MapFrom(c => c.Cep))
                .ForMember(x => x.Email, d => d.MapFrom(c => c.Email))
                .ForMember(x => x.Estado, d => d.MapFrom(c => c.Estado))
                .ForMember(x => x.Logradouro, d => d.MapFrom(c => c.Logradouro))
                .ForMember(x => x.NumeroLogradouro, d => d.MapFrom(c => c.NumeroLogradouro))
                .ForMember(x => x.Sexo, d => d.MapFrom(c => c.Sexo))
                .ForMember(x => x.Complemento, d => d.MapFrom(c => c.Complemento))
                .ForMember(x => x.Carteira, d => d.MapFrom(c => c.Carteira))
                .ForMember(x => x.DddResidencial, d => d.MapFrom(c => c.TelefoneResidencial.JustNumbers().Substring(0, 2)))
                .ForMember(x => x.TelefoneResidencial, d => d.MapFrom(c => c.TelefoneResidencial.JustNumbers().Substring(2)))
                .ForMember(x => x.DddCelular, d => d.MapFrom(c => c.TelefoneCelular.JustNumbers().Substring(0, 2)))
                .ForMember(x => x.TelefoneCelular, d => d.MapFrom(c => c.TelefoneCelular.JustNumbers().Substring(2)))
                .ForMember(x => x.DddComercial, d => d.MapFrom(c => c.TelefoneComercial.JustNumbers().Substring(0, 2)))
                .ForMember(x => x.TelefoneComercial, d => d.MapFrom(c => c.TelefoneComercial.JustNumbers().Substring(2)))
                .ForMember(x => x.DataOptOut, d => d.UseValue(DateTime.Now));


            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(x => x.Nome, d => d.MapFrom(c => c.Nome))
                .ForMember(x => x.Documento, d => d.MapFrom(c => c.Documento))
                .ForMember(x => x.DataNascimento, d => d.MapFrom(c => c.DataNascimento))
                .ForMember(x => x.Cidade, d => d.MapFrom(c => c.Cidade))
                .ForMember(x => x.Bairro, d => d.MapFrom(c => c.Bairro))
                .ForMember(x => x.Cep, d => d.MapFrom(c => c.Cep))
                .ForMember(x => x.Email, d => d.MapFrom(c => c.Email))
                .ForMember(x => x.Estado, d => d.MapFrom(c => c.Estado))
                .ForMember(x => x.Carteira, d => d.MapFrom(c => c.Carteira))
                .ForMember(x => x.Logradouro, d => d.MapFrom(c => c.Logradouro))
                .ForMember(x => x.NumeroLogradouro, d => d.MapFrom(c => c.NumeroLogradouro))
                .ForMember(x => x.Sexo, d => d.MapFrom(c => c.Sexo))
                .ForMember(x => x.Complemento, d => d.MapFrom(c => c.Complemento))
                .ForMember(x => x.TelefoneResidencial, d => d.MapFrom(c => $"{c.DddResidencial}{c.TelefoneResidencial}"))
                .ForMember(x => x.TelefoneCelular, d => d.MapFrom(c => $"{c.DddCelular}{c.TelefoneCelular}"))
                .ForMember(x => x.TelefoneComercial, d => d.MapFrom(c => $"{c.DddComercial}{c.TelefoneComercial}"))
                .ForMember(x => x.Aceite, d => d.MapFrom(c => c.DataOptOut.HasValue));

        }
    }
}
