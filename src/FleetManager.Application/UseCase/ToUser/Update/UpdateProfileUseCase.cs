using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToUser.Update
{
    public class UpdateProfileUseCase(ILoggedUser loggedUser, IUserReadOnlyRepository readOnlyRepository, IUserUpdateOnlyRepository repository,  IUnitOfWork unitOfWork) : IUpdateProfileUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUserReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        private readonly IUserUpdateOnlyRepository _updateOnlyRepository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await _loggedUser.Get();
            await Validate(request, loggedUser.Email);

            var user = await _updateOnlyRepository.GetById(loggedUser.Id);

            user.Update(request.Name, request.Email);

            _updateOnlyRepository.Update(user);

            await _unitOfWork.Commit();

          

        }
        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateValidator();
            var result = validator.Validate(request);

            if (currentEmail.Equals(request.Email) == false)
            {
                var emailAlreadyExists = await _readOnlyRepository.ExistByEmail(request.Email);
                 if (emailAlreadyExists)
                        result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }

            if (result.IsValid == false)
            {
                 var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessage);
            }
        }

    }
}
