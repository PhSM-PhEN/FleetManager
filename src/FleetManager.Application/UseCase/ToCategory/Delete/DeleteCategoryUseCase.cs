using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.Delete
{
    public class DeleteCategoryUseCase(IUnitOfWork unitOfWork, ICategoryWriteOnlyRepository repository, ICategoryReadOnlyRepository readOnlyRepository) : IDeleteCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICategoryWriteOnlyRepository _repository = repository;
        private readonly ICategoryReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        public async Task Execute(int id)
        {
            var category = await _readOnlyRepository.GetById(id);
            if (category == null)
            {
                throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);
            }
            await _repository.Delete(id);
            await _unitOfWork.Commit();
        }
    }
}
