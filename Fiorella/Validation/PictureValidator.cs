using Fiorella.Dto;
using FluentValidation;

namespace Fiorella.Validation
{
    public class PictureValidator : AbstractValidator<PictureDto>
    {
        public PictureValidator()
        {
            RuleFor(x => x.Name).MaximumLength(20);
            RuleFor(x => x.Description).MaximumLength(20);
            RuleFor(x => x.Category).MaximumLength(20);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Source).MinimumLength(0);
        }
    }
}
