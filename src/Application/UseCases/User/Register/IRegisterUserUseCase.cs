using Communication.Requests;
using Communication.Responses;

namespace Domain.UseCases.User.Register;

public interface IRegisterUserUseCase {

    Task<UserResponseRegisteredAndLoginJson> Executar(JsonUserRegistrationRequest user);

}
