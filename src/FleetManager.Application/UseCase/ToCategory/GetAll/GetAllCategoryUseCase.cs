using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToCategory;

namespace FleetManager.Application.UseCase.ToCategory.GetAll
{
    public class GetAllCategoryUseCase(IMapper mapper,
        ICategoryReadOnlyRepository categoryReadOnly) : IGetAllCategoryUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryReadOnlyRepository _categoryRepository = categoryReadOnly;
        public async Task<ResponseListCategoryJson> Execute()
        {
            var categories = await _categoryRepository.GetAll();

            return new ResponseListCategoryJson 
            {
                Categories = _mapper.Map<List<ResponseCategoryJson>>(categories)
            };


        }
    }
}
