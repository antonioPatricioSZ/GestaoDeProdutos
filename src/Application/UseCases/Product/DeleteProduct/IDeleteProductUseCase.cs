namespace Application.UseCases.Product.DeleteProduct;

public interface IDeleteProductUseCase {

    Task Executar(long productId);

}
