using AutoMapper;
using FleetManager.communication.Resposnes.ToCategory;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.GetById
{
    public class GetByIdCategoryUseCase(ICategoryReadOnlyRepository readOnlyRepository, IMapper mapper) : IGetByIdCategoryUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        public async Task<ResponseShortCategoryJson> Execute(int id)
        {
            var result = await _readOnlyRepository.GetById(id);
            if (result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND);
            }
            return _mapper.Map<ResponseShortCategoryJson>(result);
        }
    }
}
