using AutoMapper;
using Communication.Requests;
using Domain.Repositories;
using Domain.Repositories.Category;
using Domain.Repositories.Product;
using Exceptions;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Product.UpdateProduct;

public class UpdateProductUseCase : IUpdateProductUseCase {

    private readonly IProductUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;

    public UpdateProductUseCase(
        IProductUpdateOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICategoryReadOnlyRepository categoryReadOnlyRepository
    ){
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
    }


    public async Task Executar(long productId, RequestProductJson request) {

        var product = await _repository.GetById(productId);

        var categories = await _categoryReadOnlyRepository.GetAllCategories();
        var categoryIds = categories.Select(category => category.Id).ToList();

        Validate(product, request, categoryIds);

        _mapper.Map(request, product); 
        // como eu já tenho o product e já estou mapeando a request
        // eu posso fazer desse jeito

        _repository.Update(product);

        await _unitOfWork.Commit();

    }


    private static void Validate(
        Domain.Entities.Product product,
        RequestProductJson request,
        List<long> idsProducts
    ){ 

        if(product is null) {
            var mensagensDeErro = new List<string> {
                ResourceErrorMessages.PRODUTO_NAO_ENCONTRADO
            };
            throw new ValidationErrorsException(mensagensDeErro);
        }
    
        var validator = new UpdateProductValidator();
        var result = validator.Validate(request);

        if(!result.IsValid) {
            var mensagensDeErro = result.Errors.Select(erro => erro.ErrorMessage).ToList();
            throw new ValidationErrorsException(mensagensDeErro);
        }


        if (!idsProducts.Contains(request.CategoryId)) {
            var mensagensDeErro = new List<string> {
                ResourceErrorMessages.ID_CATEGORIA_INVALIDO
            };
            throw new ValidationErrorsException(mensagensDeErro);
        }

    }

}
