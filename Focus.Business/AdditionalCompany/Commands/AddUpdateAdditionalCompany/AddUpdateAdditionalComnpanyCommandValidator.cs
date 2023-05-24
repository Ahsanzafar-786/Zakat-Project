using FluentValidation;

namespace Focus.Business.AdditionalCompany.Commands.AddUpdateAdditionalCompany
{
    public class AddUpdateAdditionalComnpanyCommandValidator : AbstractValidator<AddUpdateAdditionalCompanyCommand>
    {
        public AddUpdateAdditionalComnpanyCommandValidator()
        {
            RuleFor(x => x.NameArabic).NotEmpty().MaximumLength(50);
            RuleFor(x => x.CityArabic).MaximumLength(150);
        }
    }
}
