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
                .NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
            RuleFor(x => x.BaseDailyRate)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MUST_BE_GREATER_THAN_ZERO);


        }
    }
}
