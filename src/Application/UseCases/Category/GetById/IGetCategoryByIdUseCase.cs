using Communication.Responses;

namespace Application.UseCases.Category.GetById;

public interface IGetCategoryByIdUseCase {

    Task<ResponseCategoryJson> Executar(long categoyId);

}
