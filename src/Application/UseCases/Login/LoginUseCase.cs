using System.Runtime.Intrinsics.X86;
using Application.Services.Cryptography;
using Application.Services.Token;
using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories;
using Domain.Repositories.User;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Login;


public class LoginUseCase : ILoginUseCase {


    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _repository;
    private readonly TokenController _tokenController;

    public LoginUseCase(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserReadOnlyRepository repository,
        TokenController tokenController
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
        _tokenController = tokenController;
    }

    public async Task<UserResponseRegisteredAndLoginJson> Executar(UserLoginJsonRequest request) {

        Validate(request);

        var senhaEncriptada = PasswordEncryptor.Criptografar(request.Password);

        var usuario = await _repository.Login(request.Email, senhaEncriptada) ?? throw new InvalidLoginException();
        var token = _tokenController.GenerateToken(usuario.Id, new List<string> {
            "User"
        });

        return new UserResponseRegisteredAndLoginJson {
            Token = token
        };

        // Esta é uma expressão que usa o operador de coalescência nula(??).
        // O operador de coalescência nula é usado para fornecer um valor padrão
        // caso o resultado da expressão à esquerda seja nulo.

        // No seu código, a expressão await _repository.Login(request.Email,
        // senhaEncriptada) é avaliada primeiro.Se o resultado dessa expressão
        // for nulo, então a exceção InvalidLoginException será lançada.Isso
        // significa que se a chamada ao método Login retornar null, o código
        // lançará uma exceção InvalidLoginException.

    }


    private static void Validate(UserLoginJsonRequest request) {

        var validator = new LoginValidator();
        var resultado = validator.Validate(request);

        if(!resultado.IsValid) {
            var mensagensDeErro = resultado.Errors.Select(
                erro => erro.ErrorMessage
            ).ToList();

            throw new ValidationErrorsException(mensagensDeErro);
        }

    }

}
