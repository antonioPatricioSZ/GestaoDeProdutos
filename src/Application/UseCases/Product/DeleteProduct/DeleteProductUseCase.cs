using Domain.Repositories;
using Domain.Repositories.Product;
using Exceptions;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Product.DeleteProduct;

public class DeleteProductUseCase : IDeleteProductUseCase {

    private readonly IProductWriteOnlyRepository _repositoryWriteOnly;
    private readonly IProductReadOnlyRepository _repositoryReadOnly;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductUseCase(
        IProductWriteOnlyRepository repositoryWriteOnly,
        IProductReadOnlyRepository repositoryReadOnly,
        IUnitOfWork unitOfWork
    ){
        _repositoryWriteOnly = repositoryWriteOnly;
        _repositoryReadOnly = repositoryReadOnly;
        _unitOfWork = unitOfWork;
    }

    public async Task Executar(long productId) {

        var product = await _repositoryReadOnly.GetProductById(productId);

        Validate(product);

        await _repositoryWriteOnly.Delete(productId);

        await _unitOfWork.Commit();
        
    }


    private static void Validate(Domain.Entities.Product product) {
        if(product is null) {
            var mensagensDeErro = new List<string> { 
                ResourceErrorMessages.PRODUTO_NAO_ENCONTRADO
            };
            throw new ValidationErrorsException(mensagensDeErro);
        }
    }

}
