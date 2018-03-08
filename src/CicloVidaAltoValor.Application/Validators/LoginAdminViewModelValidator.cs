using FluentValidation;
using CicloVidaAltoValor.Application.Contracts.Login;

namespace CicloVidaAltoValor.Application.Validators
{
    public class LoginAdminViewModelValidator : AbstractValidator<LoginAdminViewModel>
    {
        public LoginAdminViewModelValidator()
        {
            RuleFor(x => x.Username)
                 .NotEmpty()
                 .NotNull();

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
