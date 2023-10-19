using System.Text.RegularExpressions;
using AutoMapper;
using Azure.Storage.Blobs;
using Communication.Responses;
using Domain.Extension;
using Domain.Repositories.Product;

namespace Application.UseCases.Product.GetProducts;

public class GetProductsUseCase : IGetProductsUseCase {

    private readonly IMapper _mapper;
    private readonly IProductReadOnlyRepository _repository;
    private readonly AzureStorageStringConnection _stringCinnection;

    public GetProductsUseCase(
        IProductReadOnlyRepository repository,
        IMapper mapper,
        AzureStorageStringConnection stringCinnection
    )
    {
        _repository = repository;
        _mapper = mapper;
        _stringCinnection = stringCinnection;
    }

<<<<<<< HEAD
    public async Task<List<ResponseGetProductJson>> Executar() {   
=======
    public async Task<List<ResponseGetProductJson>> Executar() {
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 701f6ecb5f2c6ed0dab10c0420795385793d3e36
=======
>>>>>>> main
=======
>>>>>>> main
        var resultado = await _repository.GetAllProducts();
        return _mapper.Map<List<ResponseGetProductJson>>(resultado);
    }

    public string UploadBase65Image(string base64Image, string container) {
        var azureStorageConnectionString = _stringCinnection.AzureStorageConnectionString();
        var filename = Guid.NewGuid().ToString() + ".jpg";
        var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

        byte[] imageBytes = Convert.FromBase64String(data);

        var blobClient = new BlobClient(
            azureStorageConnectionString,
            container,
            filename
        );

        using (var stream = new MemoryStream(imageBytes))
        {
            blobClient.Upload(stream);
        }
        return blobClient.Uri.AbsoluteUri;
    }

}
