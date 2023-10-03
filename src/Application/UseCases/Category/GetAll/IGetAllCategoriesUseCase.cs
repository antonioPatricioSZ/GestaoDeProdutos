using Communication.Responses;

namespace Application.UseCases.Category.GetAll;

public interface IGetAllCategoriesUseCase {

    Task<List<ResponseCategoryJson>> Executar();

}
