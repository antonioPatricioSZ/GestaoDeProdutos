using Communication.Requests;
using Exceptions;
using FluentValidation;

namespace Application.UseCases.Product;

public class ProductValidator : AbstractValidator<RequestProductJson>{

    public ProductValidator() {
        RuleFor(request => request.Name).NotEmpty()
            .WithMessage(ResourceErrorMessages.NOME_PRODUTO_EMBRANCO);

        RuleFor(request => request.Description).NotEmpty()
            .WithMessage(ResourceErrorMessages.DESCRICAO_PRODUTO_EMBRANCO);

        RuleFor(request => request.PricePurchase).NotEmpty().GreaterThan(0)
            .WithMessage(ResourceErrorMessages.PRECO_COMPRA_PRODUTO_EM_BRANCO_OU_INVALIDO);

        RuleFor(request => request.PriceSale).NotEmpty().GreaterThanOrEqualTo(f => f.PricePurchase * 1.20m)
            .WithMessage(ResourceErrorMessages.PRECO_VENDA_PRODUTO_EM_BRANCO_OU_INVALIDO);

    }

}
