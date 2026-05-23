using AutoMapper;
using FleetManager.communication.Responses;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCategory.GetById
{
    public class GetByIdCategoryUseCase(ICategoryReadOnlyRepository readOnlyRepository, IMapper mapper) : IGetByIdCategoryUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        public async Task<ResponseCategoryJson> Execute(int id)
        {
            var result = await _readOnlyRepository.GetById(id);
            
            return _mapper.Map<ResponseCategoryJson>(result);
        }
    }
}
