using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.Update
{
    public class UpdateCategoryUseCase(IUnitOfWork unitOfWork, ICategoryUpdateOnlyRepository updateRepository) : IUpdateCategoryUseCase
    {


        public async Task Execute(long id, RequestCategoryJson request)
        {
            Validate(request);

            var category = await updateRepository.GetById(id)
                ?? throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);

            category.Update(request.Name, (Domain.Enums.TransmissionType)request.TransmissionType);

            updateRepository.Update(category);

            await unitOfWork.Commit();

        }
        private static void Validate(RequestCategoryJson request)
        {
            var validator = new CategoryValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
