using Communication.Requests;
using FluentValidation;

namespace Application.UseCases.Product.AddProduct;

public class AddProductValidator : AbstractValidator<RequestProductJson> {

    public AddProductValidator() {
        RuleFor(request => request).SetValidator(new ProductValidator());
    }

}
