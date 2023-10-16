using Communication.Requests;

namespace Application.UseCases.Category.Update;

public interface IUpdateCategoryUseCase {

    Task Executar(long categoryId, RequestRegisterCategoryJson request);

}
