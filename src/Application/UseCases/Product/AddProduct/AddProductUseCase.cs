using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories;
using Domain.Repositories.Category;
using Domain.Repositories.Product;
using Exceptions;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Product.AddProduct;

public class AddProductUseCase : IAddProductUseCase {

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductWriteOnlyRepository _repository;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;

    public AddProductUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IProductWriteOnlyRepository repository,
        ICategoryReadOnlyRepository categoryReadOnlyRepository
    )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
    }


    public async Task<ResponseProductJson> Executar(RequestProductJson request) {

        Validate(request);

        var product = _mapper.Map<Domain.Entities.Product>(request);
        var categories = await _categoryReadOnlyRepository.GetAllCategories();
        var categoryIds = categories.Select(category  => category.Id).ToList();

        ValidateIdToIds(categoryIds, request.CategoryId);

        await _repository.AddProduct(product);
        
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseProductJson>(product);
        
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

    private static void ValidateIdToIds(List<long> idsProducts, long reuestProductId) {
        
        if (!idsProducts.Contains(reuestProductId)) {
            var mensagensDeErro = new List<string> {
                ResourceErrorMessages.ID_CATEGORIA_INVALIDO
            };
            throw new ValidationErrorsException(mensagensDeErro);
        }
    }


}
