using System;
using FluentValidation;
using CicloVidaAltoValor.Application.Extensions;
using CicloVidaAltoValor.Application.Model.Entities;
using CicloVidaAltoValor.Application.Properties;

namespace CicloVidaAltoValor.Application.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        

        public UsuarioValidator()
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
               .NotNull()
               .WithMessage(Resources.Required)
               .NotEmpty()
               .WithMessage(Resources.Required)
               .MinimumLength(3)
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

            RuleFor(p => p.Cep)
            .NotNull()
            .WithMessage(Resources.Required)
            .NotEmpty()
            .WithMessage(Resources.Required)
            .Length(8)
            .WithMessage(Resources.Required)
            .Must(StringExtensions.IsPostalCode);


            RuleFor(p => p.DddResidencial)
                .NotEmpty()
                .WithMessage(Resources.PhoneRequired)
                .When(p =>  (!p.DddCelular.HasValue && string.IsNullOrWhiteSpace(p.TelefoneCelular)) && (!p.DddComercial.HasValue && string.IsNullOrWhiteSpace(p.TelefoneComercial)))
                .WithMessage(Resources.Required);
            
            RuleFor(p => p.DddResidencial)
                .Must(p => p > 10 && p < 100)
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneResidencial))
                .WithMessage(Resources.Required);

            RuleFor(p => p.TelefoneResidencial)
                .NotEmpty()
                .WithMessage(Resources.PhoneRequired)
                .When(p =>  (!p.DddCelular.HasValue && string.IsNullOrWhiteSpace(p.TelefoneCelular)) && (!p.DddComercial.HasValue && string.IsNullOrWhiteSpace(p.TelefoneComercial)));
            
            RuleFor(p => p.TelefoneResidencial)
                .Must(Extensions.StringExtensions.IsInteger)
                .When(p => p.DddResidencial.HasValue)
                .WithMessage(Resources.Required);

            RuleFor(p => p.DddCelular)
                .NotEmpty()
                .WithMessage(Resources.PhoneRequired)
                .When(p => (!p.DddResidencial.HasValue && string.IsNullOrWhiteSpace(p.TelefoneResidencial)) && (!p.DddComercial.HasValue && string.IsNullOrWhiteSpace(p.TelefoneComercial)));
                
            RuleFor(p => p.DddCelular)
                .Must(p => p > 10 && p < 100)
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneCelular))
                .WithMessage(Resources.Required);


            RuleFor(p => p.TelefoneCelular)
                .NotEmpty()
                .WithMessage(Resources.PhoneRequired)
                .When(p => (!p.DddResidencial.HasValue && string.IsNullOrWhiteSpace(p.TelefoneResidencial)) && (!p.DddComercial.HasValue && string.IsNullOrWhiteSpace(p.TelefoneComercial)));
            
            RuleFor(p => p.TelefoneCelular)
                .Must(Extensions.StringExtensions.IsInteger)
                .When(p => p.DddCelular.HasValue)
                .WithMessage(Resources.Required);


            RuleFor(p => p.DddComercial)
                .NotEmpty()
                .WithMessage(Resources.PhoneRequired)
                .When(p => (!p.DddResidencial.HasValue && string.IsNullOrWhiteSpace(p.TelefoneResidencial)) && (!p.DddCelular.HasValue && string.IsNullOrWhiteSpace(p.TelefoneCelular)));
            
            RuleFor(p => p.DddComercial).Must(p => p > 10 && p < 100)
                .When(p => !string.IsNullOrWhiteSpace(p.TelefoneComercial))
                .WithMessage(Resources.Required);

            RuleFor(p => p.TelefoneComercial)
                .NotEmpty()
                .WithMessage(Resources.PhoneRequired)
                .When(p => (!p.DddResidencial.HasValue && string.IsNullOrWhiteSpace(p.TelefoneResidencial)) && (!p.DddCelular.HasValue && string.IsNullOrWhiteSpace(p.TelefoneCelular)));

            RuleFor(p => p.TelefoneComercial)
                .Must(Extensions.StringExtensions.IsInteger)
                .When(p => p.DddComercial.HasValue)
                .WithMessage(Resources.Required);

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


        }
    }
}
