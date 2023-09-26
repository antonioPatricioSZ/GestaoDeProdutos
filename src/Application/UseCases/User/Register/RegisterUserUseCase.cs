using System.Diagnostics.CodeAnalysis;
using Application.Services.Cryptography;
using Application.Services.Token;
using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories;
using Domain.Repositories.User;
using Domain.UseCases.User.Register;
using Exceptions;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase {

    private readonly IUserWriteOnlyRepository _repository;
    private readonly IUserReadOnlyRepository _repositoryReadOnly;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly TokenController _tokenController;

    public RegisterUserUseCase(
        IUnitOfWork unitOfWork,
        IMapper mapper, 
        IUserWriteOnlyRepository repository,
        TokenController tokenController,
        IUserReadOnlyRepository repositoryReadOnly
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
        _tokenController = tokenController;
        _repositoryReadOnly = repositoryReadOnly;
    }

    public async Task<UserResponseRegisteredAndLoginJson> Executar(JsonUserRegistrationRequest requestUser) {

        await Validator(requestUser);

        var entidade = _mapper.Map<Domain.Entities.User>(requestUser);
        entidade.Password = PasswordEncryptor.Criptografar(requestUser.Password);

        await _repository.Register(entidade);
        await _unitOfWork.Commit();

        var token = _tokenController.GenerateToken(entidade.Id, new List<string> {
            "Admin",
            "Manager"
        });

        return new UserResponseRegisteredAndLoginJson
        {
            Token = token,
        };

    }


    private async Task Validator(JsonUserRegistrationRequest requestUser) {
        var validator = new RegisterUserValidator();
        var resultado = validator.Validate(requestUser);


        var userEmailExists = await _repositoryReadOnly.UserEmailExists(requestUser.Email);
        if (userEmailExists) {
            resultado.Errors.Add(
                new FluentValidation.Results.ValidationFailure(
                    "email", ResourceErrorMessages.EMAIL_JA_REGISTRADO
                )
            );
        }


        if(!resultado.IsValid) {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(mensagensDeErro);
        }
    }

}
