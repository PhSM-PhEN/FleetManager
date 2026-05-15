using AutoMapper;
using FleetManager.communication.Requests.ToUser;
using FleetManager.communication.Resposnes.ToUsers;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Security.Token;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToUser.Register
{
    public class RegisterUserUseCase(IUnitOfWork unitOfWork, IUserWriteOnlyRepository repository,
        IUserReadOnlyRepository userReadOnly,
        IPasswordEncripter encripter,IMapper mapper, IAccesTokenGenerator tokenGenerator) : IRegisterUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserWriteOnlyRepository _repository = repository;
        private readonly IUserReadOnlyRepository _userReadOnly = userReadOnly;
        private readonly IPasswordEncripter _encripter = encripter;
        private readonly IAccesTokenGenerator _tokenGenerator = tokenGenerator;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request); 
            
            var user = _mapper.Map<User>(request);
            user.Password = _encripter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _repository.Add(user);
            await _unitOfWork.Commit();

            return new ResponseRegisterUserJson
            {
                Name = user.Name,
                Token = _tokenGenerator.GenerateToken(user)

            };
        }
        private async Task Validate(RequestRegisterUserJson request)
        {
            var validate = new UserValidator();
            var result = validate.Validate(request);

            var emailExists = await _userReadOnly.ExistByEmail(request.Email);

            if (emailExists)
            {
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
