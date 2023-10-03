using Communication.Responses;

namespace Application.UseCases.Product.GetById;

public interface IGetProductByIdUseCase {

    Task<ResponseGetProductJson> Executar(long productId);

}
