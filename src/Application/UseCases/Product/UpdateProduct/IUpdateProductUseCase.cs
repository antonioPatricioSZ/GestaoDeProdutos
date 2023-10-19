using Communication.Requests;

namespace Application.UseCases.Product.UpdateProduct;

public interface IUpdateProductUseCase {

    Task Executar(long productId, RequestProductJson request);

}
