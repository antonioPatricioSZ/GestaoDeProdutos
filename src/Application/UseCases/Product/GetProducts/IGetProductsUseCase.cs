using Communication.Responses;

namespace Application.UseCases.Product.GetProducts;

public interface IGetProductsUseCase {

    Task<List<ResponseGetProductJson>> Executar();

}
