using System.Net;
using Communication.Responses;
using Exceptions;
using Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class ExceptionsFilter : IExceptionFilter {

    public void OnException(ExceptionContext context) {
        
        if(context.Exception is GestaoDeProdutosException) {
            HandleGestaoDeProdutosException(context);
        } else {
            ThrowUnknownError(context);
        }

    }


    private static void HandleGestaoDeProdutosException(ExceptionContext context) {
        if(context.Exception is ValidationErrorsException) {
            HandleValidationErrorsException(context);
        }

        if(context.Exception is InvalidLoginException) {
            HandleInvalidLoginException(context);
        }
    }


    private static void HandleValidationErrorsException(ExceptionContext context) {
        var errosDeValidacaoException = context.Exception as ValidationErrorsException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(
            new ResponseErrorJson(errosDeValidacaoException.ErrorMessages)
        );
    }


    private static void HandleInvalidLoginException(ExceptionContext context) {
        var invalidLoginException = context.Exception as InvalidLoginException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

        context.Result = new ObjectResult(
            new ResponseErrorJson(invalidLoginException.Message)
        );

    }


    private static void ThrowUnknownError (ExceptionContext context) {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult (
            new Exception(context.Exception.Message)
        );
    }


}
