using AutoMapper;
using Communication.Requests;
using Domain.Repositories;
using Domain.Repositories.Category;
using Exceptions;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Category.Update;

public class UpdateCategoryUseCase : IUpdateCategoryUseCase {

    private readonly ICategoryUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryUseCase(
        ICategoryUpdateOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    ){
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task Executar(long categoryId, RequestRegisterCategoryJson request) {

        var category = await _repository.GetById(categoryId);

        Validate(category, request);

        _mapper.Map(request, category);

        _repository.Update(category);

        await _unitOfWork.Commit();
    
    }


    private static void Validate(Domain.Entities.Category category, RequestRegisterCategoryJson request) {

        if (category is null) {
            var mensagensDeErro = new List<string> {
                ResourceErrorMessages.PRODUTO_NAO_ENCONTRADO
            };
            throw new ValidationErrorsException(mensagensDeErro);
        }

        var validator = new UpdateCategoryValidator();
        var result = validator.Validate(request);

        if (!result.IsValid) {
            var mensagensDeErro = result.Errors.Select(erro => erro.ErrorMessage).ToList();
            throw new ValidationErrorsException(mensagensDeErro);
        }

    }

}
