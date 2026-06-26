using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.Delete
{
    public class DeleteCategoryUseCase(IUnitOfWork unitOfWork, ICategoryUpdateOnlyRepository repository) : IDeleteCategoryUseCase
    {

        public async Task Execute(long id)
        {
            var category = await repository.GetById(id);
            if (category == null)
            {
                throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);
            }
            category.Disable();
            repository.Update(category);
            await unitOfWork.Commit();
        }
    }
}
