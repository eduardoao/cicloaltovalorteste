using FluentValidation;
using CicloVidaAltoValor.Application.Contracts.Desejos;
using CicloVidaAltoValor.Application.Properties;

namespace CicloVidaAltoValor.Application.Validators
{
    public class DesejoProdutoViewModelValidator : AbstractValidator<DesejoProdutoViewModel>
    {
        public DesejoProdutoViewModelValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(Resources.Required);
                

            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage(Resources.Required);


            RuleFor(x => x.CampanhaProdutoId)
                .NotEmpty()
                .WithMessage(Resources.Required)
                .NotNull()
                .WithMessage(Resources.Required)
                .InclusiveBetween(1, int.MaxValue);

            RuleFor(x => x.ProdutoId)
                .NotEmpty()
                .WithMessage(Resources.Required)
                .NotNull()
                .WithMessage(Resources.Required)
                .InclusiveBetween(1, int.MaxValue);

        }
    }
}
