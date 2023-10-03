using AutoMapper;
using Communication.Responses;
using Domain.Repositories.Category;
using Exceptions;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Category.GetById;

public class GetCategoryByIdUseCase : IGetCategoryByIdUseCase {

    private readonly ICategoryReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryByIdUseCase(
        ICategoryReadOnlyRepository repository,
        IMapper mapper
    ){
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseCategoryJson> Executar(long categoyId) {
        
        var category = await _repository.GetCategoryById(categoyId);

        Validate(category);

        return _mapper.Map<ResponseCategoryJson>(category); 

    }


    private static void Validate(Domain.Entities.Category category) {
        if(category is null) {
            var mensagensDeErro = new List<string> {
                ResourceErrorMessages.CATEGORIA_NAO_ENCONTRADA
            };

            throw new ValidationErrorsException(mensagensDeErro);
        }
    }

}
