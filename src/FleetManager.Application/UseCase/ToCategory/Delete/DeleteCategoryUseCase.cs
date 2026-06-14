using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.Delete
{
    public class DeleteCategoryUseCase(IUnitOfWork unitOfWork, ICategoryUpdateOnlyRepository repository) : IDeleteCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICategoryUpdateOnlyRepository _repository = repository;

        public async Task Execute(int id)
        {
            var category = await _repository.GetById(id);
            if (category == null)
            {
                throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);
            }
            category.Disable();
            _repository.Update(category);
            await _unitOfWork.Commit();
        }
    }
}
