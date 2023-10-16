using Communication.Requests;
using FluentValidation;

namespace Application.UseCases.Product.UpdateProduct;


public class UpdateProductValidator : AbstractValidator<RequestProductJson> {

    public UpdateProductValidator() {
        RuleFor(request => request).SetValidator(new ProductValidator());
    }

}
