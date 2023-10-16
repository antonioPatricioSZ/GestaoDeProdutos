using Communication.Requests;
using Exceptions;
using FluentValidation;

namespace Application.UseCases.Category.Register;

public class RegisterCategoryValidator : AbstractValidator<RequestRegisterCategoryJson> {

    public RegisterCategoryValidator() {
        
    }

}
