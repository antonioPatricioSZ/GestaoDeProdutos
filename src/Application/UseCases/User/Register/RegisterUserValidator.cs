using System.Text.RegularExpressions;
using Communication.Requests;
using Exceptions;
using FluentValidation;

namespace Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<JsonUserRegistrationRequest> {

    public RegisterUserValidator() {

        RuleFor(request => request.Name).NotEmpty()
            .WithMessage(ResourceErrorMessages.NOME_USUARIO_EM_BRANCO);


        RuleFor(request => request.Email).NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_EM_BRANCO);

        
        RuleFor(request => request.PhoneNumber).NotEmpty()
            .WithMessage(ResourceErrorMessages.TELEFONE_USUARIO_EM_BRANCO);


        RuleFor(request => request.BirthDate).NotEmpty()
            .WithMessage(ResourceErrorMessages.DATA_NASCIMENTO_EM_BRANCO);


        RuleFor(request => request.Password).SetValidator(new PasswordValidator());


        When(request => !string.IsNullOrWhiteSpace(request.Email), () => {
            RuleFor(request => request.Email).EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_INVALIDO);
        });


        When(request => !string.IsNullOrWhiteSpace(request.PhoneNumber), () => {
            RuleFor(request => request.PhoneNumber).Custom((phoneNumber, context) => {
                var telefonePadrao = "(\\d{2}\\s?9\\s?\\d{4}-?\\d{4})";
                var isMatch = Regex.IsMatch(phoneNumber, telefonePadrao);
                if(!isMatch) {
                    context.AddFailure(
                        new FluentValidation.Results.ValidationFailure(
                            nameof(phoneNumber),
                            ResourceErrorMessages.TELEFONE_USUARIO_INVALIDO
                        )
                    );
                }
            });
        });

    }

}
