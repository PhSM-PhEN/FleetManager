using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggeUser;

namespace FleetManager.Application.UseCase.ToUser.Delete
{
    public class DeleteUserAccountUseCase(IUserWriteOnlyRepository repository, ILoggedUser loggedUser, IUnitOfWork unitOfWork) : IDeleteUserAccountUseCase
    {
        private readonly IUserWriteOnlyRepository _repository = repository;
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Execute()
        {
            var user = await _loggedUser.Get();

            await _repository.Delete(user);
            await _unitOfWork.Commit();

        }
    }
}
