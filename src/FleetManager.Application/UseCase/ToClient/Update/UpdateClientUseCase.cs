using AutoMapper;
using FleetManager.communication.Requests.ToClient;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToClient.Update
{
    public class UpdateClientUseCase(IClientUpdateOnlyRepository updateRepository,  IUnitOfWork unitOfWork, IMapper mapper) : IUpdateClientUseCase
    {
        private readonly IClientUpdateOnlyRepository _updateRepository = updateRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task Execute(long id, RequestClientJson request)
        {
            Validate(request);
            var client = await _updateRepository.GetById(id);
            if (client == null)
            {
                throw new NotFoundException(ResourceErrorMessages.CLIENT_NOT_FOUND);
            }
            _mapper.Map(request, client);

            _updateRepository.Update(client);
            await _unitOfWork.Commit();


        }
        private static void Validate(RequestClientJson request)
        {
            var validator = new ClientValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }

}
