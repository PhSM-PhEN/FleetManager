using AutoMapper;
using FleetManager.communication.Requests;
using FleetManager.communication.Resposnes;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.Register
{
    public class RegisterCategoryUseCase(ICategoryWriteOnlyRepository categoryWriteOnly,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRegisterCategoryUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryWriteOnlyRepository _categoryWriteOnlyRepository = categoryWriteOnly;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseShortCategoryJson> Execute(RequestCategoryJson request)
        {
            Validate(request);

            var category = _mapper.Map<Category>(request);

            await _categoryWriteOnlyRepository.Add(category);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseShortCategoryJson>(category);

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
