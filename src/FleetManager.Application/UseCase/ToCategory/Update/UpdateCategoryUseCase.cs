using AutoMapper;
using FleetManager.communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.Update
{
    public class UpdateCategoryUseCase(IUnitOfWork unitOfWork, ICategoryUpdateOnlyRepository updateRepository,
        IMapper mapper) : IUpdateCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICategoryUpdateOnlyRepository _updateRepository = updateRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Execute(int id, RequestCategoryJson request)
        {
            Validate(request);

            var category = await _updateRepository.GetById(id);

            if (category is null)
            {
                throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);
            }
            _mapper.Map(request, category);
            _updateRepository.Update(category);

            await _unitOfWork.Commit();

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
