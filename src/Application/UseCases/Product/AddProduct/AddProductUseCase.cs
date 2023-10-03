using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories;
using Domain.Repositories.Product;
using Exceptions;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Product.AddProduct;

public class AddProductUseCase : IAddProductUseCase {

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductWriteOnlyRepository _repository;

    public AddProductUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IProductWriteOnlyRepository repository
    ){
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }


    public async Task<ResponseProductJson> Executar(RequestProductJson request) {

        Validate(request);
        var product = _mapper.Map<Domain.Entities.Product>(request);

        var result = await _repository.AddProduct(product, 3);
        IsError(request, result);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseProductJson>(product);
        
    }


    private static void IsError(RequestProductJson request, bool result) {
        var validator = new AddProductValidator();
        var resultado = validator.Validate(request);

        if (result) {
            resultado.Errors.Add(
                new FluentValidation.Results.ValidationFailure(
                    "categoryId",
                    ResourceErrorMessages.CATEGORIA_INVALIDA
                )
            );
        }

    }


    private static void Validate(RequestProductJson request) {
        var validator = new AddProductValidator();
        var resultado = validator.Validate(request);     

        if(!resultado.IsValid) {
            var mensagensDeErro = resultado.Errors.Select(
                error => error.ErrorMessage
            ).ToList();
            throw new ValidationErrorsException(mensagensDeErro);
        }
    }

}
