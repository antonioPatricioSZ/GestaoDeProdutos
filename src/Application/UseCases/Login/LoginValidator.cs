using Application.UseCases.User;
using Communication.Requests;
using Exceptions;
using FluentValidation;

namespace Application.UseCases.Login;

public class LoginValidator : AbstractValidator<UserLoginJsonRequest> {

    public LoginValidator() {

        RuleFor(request => request.Email).NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_EM_BRANCO);

        When(request => !string.IsNullOrWhiteSpace(request.Email), () => {
            RuleFor(request => request.Email).EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_INVALIDO);
        });

        RuleFor(request => request.Password).SetValidator(new PasswordValidator());

    }

}
