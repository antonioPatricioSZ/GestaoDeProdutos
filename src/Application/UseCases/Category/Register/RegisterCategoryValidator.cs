using Communication.Requests;
using Exceptions;
using FluentValidation;

namespace Application.UseCases.Category.Register;

public class RegisterCategoryValidator : AbstractValidator<RequestRegisterCategoryJson> {

    public RegisterCategoryValidator() {
        RuleFor(request => request.CategoryName).NotEmpty()
            .WithMessage(ResourceErrorMessages.NOME_CATEGORIA_EM_BRANCO);

        RuleFor(request => request.Description).NotEmpty()
            .WithMessage(ResourceErrorMessages.DESCRICAO_CATEGORIA_EM_BRANCO);
    }

}
