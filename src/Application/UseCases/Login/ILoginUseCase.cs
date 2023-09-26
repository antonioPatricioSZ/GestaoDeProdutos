using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.Login;

public interface ILoginUseCase {

    Task<UserResponseRegisteredAndLoginJson> Executar(UserLoginJsonRequest request);

}
