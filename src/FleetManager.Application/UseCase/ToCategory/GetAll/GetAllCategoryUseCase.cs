using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.GetAll
{
    public class GetAllCategoryUseCase(ICategoryReadOnlyRepository categoryReadOnly) : IGetAllCategoryUseCase
    {
        public async Task<List<ResponseCategoryJson>> Execute()
        {
            var category = await categoryReadOnly.GetAll()
                ?? throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);

            return category.ToResponse();
        }
    }
}
