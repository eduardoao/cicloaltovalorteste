using System;
using FluentValidation;
using CicloVidaAltoValor.Application.Contracts.Usuario;
using CicloVidaAltoValor.Application.Extensions;
using CicloVidaAltoValor.Application.Properties;
using StringExtensions = CicloVidaAltoValor.Application.Extensions.StringExtensions;

namespace CicloVidaAltoValor.Application.Validators
{
    public class UsuarioViewModelValidator : AbstractValidator<UsuarioViewModel>
    {
        public UsuarioViewModelValidator()
        {

           RuleFor(x => x.Documento)
               .NotNull()
               .WithMessage(Resources.Required)
               .NotEmpty()
               .WithMessage(Resources.Required)
               .Must(StringExtensions.IsCpf)
               .WithMessage(Resources.CPFInvalid)
               .When(p => !string.IsNullOrEmpty(p.Documento));



             RuleFor(p => p.Nome)
               .NotNull()
               .WithMessage(Resources.Required)
               .NotEmpty()
               .WithMessage(Resources.Required);


            RuleFor(p => p.DataNascimento).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(Resources.Required)
                .Must(p => p != default(DateTime));


            RuleFor(p => p.Logradouro)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage(Resources.Required)
                .NotNull()
                .WithMessage(Resources.Required);


              RuleFor(p => p.Bairro)
               .NotNull()
               .WithMessage(Resources.Required)
               .NotEmpty()
               .WithMessage(Resources.Required)
               .MinimumLength(3)
               .WithMessage(Resources.Required);

             RuleFor(p => p.Cidade)
               .NotNull()
               .WithMessage(Resources.Required)
               .NotEmpty()
               .WithMessage(Resources.Required)
               .MinimumLength(3)
               .WithMessage(Resources.Required);


            RuleFor(p => p.Complemento)
                .MaximumLength(100)
                .WithMessage(Resources.LengthInvalid)
                .When(p => !string.IsNullOrWhiteSpace(p.Complemento));

            //RuleFor(p => p.NumeroLogradouro)
            //.NotEmpty()
            //.WithMessage(Resources.Required)
            //.When(x=> !string.IsNullOrEmpty(x))
            //.Must(StringExtensions.IsInteger);

            RuleFor(p => p.Cep)
                .NotNull()
                .WithMessage(Resources.Required)
                .NotEmpty()
                .WithMessage(Resources.Required)
                .Length(9)
                .WithMessage(Resources.Required)
                .Must(StringExtensions.IsPostalCode);

            RuleFor(p => p.TelefoneResidencial)
                .NotEmpty()
                .WithMessage(Resources.Required)
                .When(p =>  !string.IsNullOrWhiteSpace(p.TelefoneResidencial))
                .Length(14, 15)
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneResidencial))
                .WithMessage(Resources.LengthInvalid)
                .Must(p => p.Length >= 2 && p.JustNumbers().Substring(0, 2).IsInteger())
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneResidencial) && p.TelefoneResidencial.JustNumbers().Length >= 2)
                .Must(p => int.Parse(p.JustNumbers().Substring(0, 2)) > 10 && int.Parse(p.JustNumbers().Substring(0, 2)) < 100)
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneResidencial));

            RuleFor(p => p.TelefoneResidencial)
                .NotEmpty()
                .WithMessage(Resources.PhoneRequired)
                .When(p =>  string.IsNullOrWhiteSpace(p.TelefoneCelular) && string.IsNullOrWhiteSpace(p.TelefoneComercial));

            RuleFor(p => p.TelefoneCelular)
                .NotEmpty()
                .WithMessage(Resources.Required)
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneCelular))
                .Length(14, 15)
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneCelular))
                .WithMessage(Resources.LengthInvalid)
                .Must(p => p.Length >= 2 && p.JustNumbers().Substring(0, 2).IsInteger())
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneCelular) && p.TelefoneCelular.JustNumbers().Length >= 2)
                .Must(p => int.Parse(p.JustNumbers().Substring(0, 2)) > 10 && int.Parse(p.JustNumbers().Substring(0, 2)) < 100)
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneCelular));

            
            RuleFor(p => p.TelefoneCelular)
                .NotEmpty()
                .WithMessage(Resources.PhoneRequired)
                .When(p =>  string.IsNullOrWhiteSpace(p.TelefoneResidencial) && string.IsNullOrWhiteSpace(p.TelefoneResidencial));

            RuleFor(p => p.TelefoneComercial)
               .NotEmpty()
               .WithMessage(Resources.Required)
               .When(p => !string.IsNullOrWhiteSpace(p.TelefoneComercial))
               .Length(14, 15)
               .When(p => !string.IsNullOrWhiteSpace(p.TelefoneComercial))
               .WithMessage(Resources.LengthInvalid)
               .Must(p => p.Length >= 2 && p.JustNumbers().Substring(0, 2).IsInteger())
               .When(p => !string.IsNullOrWhiteSpace(p.TelefoneComercial) && p.TelefoneComercial.JustNumbers().Length >= 2)
               .Must(p => int.Parse(p.JustNumbers().Substring(0, 2)) > 10 && int.Parse(p.JustNumbers().Substring(0, 2)) < 100)
               .When(p => !string.IsNullOrWhiteSpace(p.TelefoneComercial));

            RuleFor(p => p.TelefoneComercial)
                .NotEmpty()
                .WithMessage(Resources.PhoneRequired)
                .When(p => string.IsNullOrWhiteSpace(p.TelefoneResidencial) && string.IsNullOrWhiteSpace(p.TelefoneCelular));

            RuleFor(p => p.Email)
                .EmailAddress()
                .Must(StringExtensions.BeAnEmail)
                .When(p => !string.IsNullOrEmpty(p.Email));

            RuleFor(p => p.Estado)
                .NotNull()
                .WithMessage(Resources.Required)
                .NotEmpty()
                .WithMessage(Resources.Required)
                .Must(StringExtensions.IsState)
                .WithMessage(Resources.StateInvalid)
                .When(p => !string.IsNullOrEmpty(p.Estado));

            //RuleFor(p => p.Aceite)
            //    .Equal(true)
            //    .WithMessage(Resources.OptinRequired);

        }
    }
}