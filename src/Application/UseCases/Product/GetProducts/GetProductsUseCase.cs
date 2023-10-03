using AutoMapper;
using Communication.Responses;
using Domain.Repositories.Product;

namespace Application.UseCases.Product.GetProducts;

public class GetProductsUseCase : IGetProductsUseCase {

    private readonly IMapper _mapper;
    private readonly IProductReadOnlyRepository _repository;

    public GetProductsUseCase(
        IProductReadOnlyRepository repository,
        IMapper mapper
    ){
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ResponseGetProductJson>> Executar() {
        var resultado = await _repository.GetAllProducts();
        return _mapper.Map<List<ResponseGetProductJson>>(resultado);
    }

    public void Teste() {

    }
}
