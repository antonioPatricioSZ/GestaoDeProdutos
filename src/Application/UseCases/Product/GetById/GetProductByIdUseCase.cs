using AutoMapper;
using Communication.Responses;
using Domain.Repositories.Product;
using Exceptions;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Product.GetById;

public class GetProductByIdUseCase : IGetProductByIdUseCase {

    private readonly IProductReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdUseCase(IProductReadOnlyRepository repository, IMapper mapper) {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseGetProductJson> Executar(long productId) {

        var product = await _repository.GetProductById(productId);

        Validate(product);

        return _mapper.Map<ResponseGetProductJson>(product);

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
