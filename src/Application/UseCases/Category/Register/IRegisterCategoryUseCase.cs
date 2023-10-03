using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.Category.Register;

public interface IRegisterCategoryUseCase {

    Task<ResponseCategoryJson> Executar(RequestRegisterCategoryJson request);

}
