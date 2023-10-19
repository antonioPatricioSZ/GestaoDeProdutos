using Communication.Requests;
using Exceptions;
using FluentValidation;

namespace Application.UseCases.Category;

public class CategoryValidator : AbstractValidator<RequestRegisterCategoryJson> {

    public CategoryValidator() {
        RuleFor(request => request.CategoryName).NotEmpty()
            .WithMessage(ResourceErrorMessages.NOME_CATEGORIA_EM_BRANCO);

        RuleFor(request => request.Description).NotEmpty()
            .WithMessage(ResourceErrorMessages.DESCRICAO_CATEGORIA_EM_BRANCO);
    }

}
