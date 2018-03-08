using System;
using CicloVidaAltoValor.Application.Contracts.Login;
using FluentValidation;
using CicloVidaAltoValor.Application.Extensions;
using CicloVidaAltoValor.Application.Properties;

namespace CicloVidaAltoValor.Application.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage(Resources.Required);
                
            RuleFor(x => x.Cpf)
                .Must(x => x.IsCpf())
                .WithMessage(Resources.CPFInvalid);

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .WithMessage(Resources.Required);

            RuleFor(x => x.DataNascimento)
                .Must(x =>  x.Date >= DateTime.Now.AddYears(-99).Date && x.Date <= DateTime.Now.AddYears(-16).Date)
                .WithMessage(Resources.DateBirthDateInvalid);
            

        }
    }
}
