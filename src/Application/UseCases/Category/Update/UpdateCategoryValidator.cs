using Communication.Requests;
using FluentValidation;

namespace Application.UseCases.Category.Update;

public class UpdateCategoryValidator : AbstractValidator<RequestRegisterCategoryJson> {

    public UpdateCategoryValidator() {
        RuleFor(request => request).SetValidator(new CategoryValidator());
            
    }

}
