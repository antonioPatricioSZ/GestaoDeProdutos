using Communication.Responses;

namespace Application.UseCases.Product.GetProducts;

public interface IGetProductsUseCase {

    Task<List<ResponseGetProductJson>> Executar();
    string UploadBase65Image(string base64Image, string container);

}
