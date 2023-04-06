using Fiorella.Dto.Login;
using FluentValidation;

namespace Fiorella.Validation
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).Length(0, 20);
            RuleFor(x => x.Password).MaximumLength(15);
        }
    }
}
