using Application.Services.Token;
using Communication.Responses;
using Domain.Repositories.User;
using Exceptions;
using Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Api.Filters.UserLogged;

public class AuthenticatedUserAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter {

    private readonly IUserReadOnlyRepository _repository;
    private readonly TokenController _tokenController;

    public AuthenticatedUserAttribute(
        IUserReadOnlyRepository repository,
        TokenController tokenController
    ){
        _repository = repository;
        _tokenController = tokenController;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context) {
          
        try {
            var token = TokenInRequest(context);
            var idUsuario = _tokenController.UserIdInToken(token);

            var user = await _repository.RecoverById(idUsuario) 
                ?? throw new GestaoDeProdutosException(string.Empty);
        }
        catch (SecurityTokenExpiredException) {
            ExpiredToken(context);
        } catch {
            UserWithoutPermission(context);
        }

    }


    private static string TokenInRequest(AuthorizationFilterContext context) {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString()
            ?? throw new GestaoDeProdutosException(string.Empty); ;

        return authorization.Split(" ")[1]; ;
    }


    private static void ExpiredToken(AuthorizationFilterContext context) {
        context.Result = new UnauthorizedObjectResult(
            new ResponseErrorJson(ResourceErrorMessages.TOKEN_EXPIRADO)
        );
    }


    private static void UserWithoutPermission(AuthorizationFilterContext context) {
        context.Result = new UnauthorizedObjectResult(
            new ResponseErrorJson(ResourceErrorMessages.USUARIO_SEM_PERMISSAO)
        );
    }

}
