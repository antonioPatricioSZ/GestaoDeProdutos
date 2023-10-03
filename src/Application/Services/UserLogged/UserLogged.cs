using Application.Services.Token;
using Domain.Entities;
using Domain.Repositories.User;
using Microsoft.AspNetCore.Http;

namespace Application.Services.UserLogged;

public class UserLogged : IUserLogged {

    private readonly IHttpContextAccessor _httpContextAccessor;
    // lembrar de colocar o httpContextAccessor no Program.cs
    private readonly TokenController _tokenController;
    private readonly IUserReadOnlyRepository _repository;

    public UserLogged(
        IUserReadOnlyRepository repository,
        TokenController tokenController,
        IHttpContextAccessor httpContextAccessor
    ){
        _repository = repository;
        _tokenController = tokenController;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<User> RecoverUser() {

        var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"]
            .ToString();

        var token = authorization["Bearer".Length..].Trim();

        var idUsuario = _tokenController.UserIdInToken(token);

        var user = await _repository.RecoverById(idUsuario);

        return user;
    }

}
