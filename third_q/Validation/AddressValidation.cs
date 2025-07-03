using FluentValidation;
using net2_third.Domain;

namespace net2_third.Validation
{
    public class AddressValidation : AbstractValidator<AddressModal>
    {
        public AddressValidation()
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("ZipCode must be 4 to 6 digits");

            When(x => x.Country?.ToLower() == "georgia", () =>
            {
                RuleFor(x => x.Street)
                    .NotEmpty().WithMessage("Street is required for Georgia");
            });
        }

    }
}
