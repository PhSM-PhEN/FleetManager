using FleetManager.communication.Requests;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToCategory
{
    public class CategoryValidator : AbstractValidator<RequestCategoryJson>
    {
        public CategoryValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_REQUIRED);
            RuleFor(x => x.TransmissionType)
                .IsInEnum().WithMessage(ResourceErrorMessages.INVALID_TRANSMISSION_TYPE);


        }
    }
}
