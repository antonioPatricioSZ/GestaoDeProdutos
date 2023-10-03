using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.Product.AddProduct;

public interface IAddProductUseCase {

    Task<ResponseProductJson> Executar(RequestProductJson request);

}
