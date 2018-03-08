
using FluentValidation;
using CicloVidaAltoValor.Application.Contracts.CepTotal;
using CicloVidaAltoValor.Application.Extensions;
using CicloVidaAltoValor.Application.Properties;
namespace CicloVidaAltoValor.Application.Validators
{
    public class CepTotalRequestValidator : AbstractValidator<CepTotalRequest>
    {
        public CepTotalRequestValidator()
        {

            RuleFor(p => p.Cep)
                 .NotEmpty()
                 .NotNull()
                 .MaximumLength(9)
                 .MinimumLength(8)
                 .WithMessage(Resources.Required)
                 .Must(StringExtensions.IsPostalCode);
        }
    }
}