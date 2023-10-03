using AutoMapper;
using Communication.Responses;
using Domain.Repositories.Category;

namespace Application.UseCases.Category.GetAll;

public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase {

    private readonly IMapper _mapper;
    private readonly ICategoryReadOnlyRepository _repository;

    public GetAllCategoriesUseCase(
        IMapper mapper,
        ICategoryReadOnlyRepository repository
    ){
        _mapper = mapper;
        _repository = repository;
    }


    public async Task<List<ResponseCategoryJson>> Executar() {
        var categorias = await _repository.GetAllCategories();

        return _mapper.Map<List<ResponseCategoryJson>>(categorias);
    }

   
}
