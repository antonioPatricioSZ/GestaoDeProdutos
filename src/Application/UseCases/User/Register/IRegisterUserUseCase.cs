using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{

    Task<UserResponseRegisteredAndLoginJson> Executar(JsonUserRegistrationRequest user);

}
