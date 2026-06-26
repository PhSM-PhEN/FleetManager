using FleetManager.Application.Extensions;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.Register
{
    public class RegisterCategoryUseCase(ICategoryWriteOnlyRepository categoryWriteOnly,
        IUnitOfWork unitOfWork) : IRegisterCategoryUseCase
    {

        public async Task<ResponseCategoryJson> Execute(RequestCategoryJson request)
        {
            Validate(request);

            var category = new Category(request.Name, (Domain.Enums.TransmissionType)request.TransmissionType);
            await categoryWriteOnly.Add(category);
            await unitOfWork.Commit();

            return category.ToResponse();

        }
        private static void Validate(RequestCategoryJson request)
        {
            var validator = new CategoryValidator();

            var ValidationResult = validator.Validate(request);

            if (ValidationResult.IsValid == false)
            {
                var errorMessage = ValidationResult.Errors.Select(erros => erros.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
