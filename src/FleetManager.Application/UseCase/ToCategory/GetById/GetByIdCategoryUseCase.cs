using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.GetById
{
    public class GetByIdCategoryUseCase(ICategoryReadOnlyRepository readOnlyRepository) : IGetByIdCategoryUseCase
    {
        
        public async Task<ResponseCategoryJson> Execute(long id)
        {
            var category = await readOnlyRepository.GetById(id) 
                ?? throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);

            return category.ToResponse();
        }
    }
}
