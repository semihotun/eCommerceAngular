using eCommerceBase.Application.Handlers.Brands.Commands;
using eCommerceBase.Domain.Resources;
using FluentValidation;

namespace eCommerceBase.Application.Handlers.Brands.Validators
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage(LanguageException.GetKey("Name is not Empty"));
        }
    }
}
