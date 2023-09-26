using Communication.Requests;
using Communication.Responses;
using Domain.UseCases.User.Register;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("api/vi/[controller]")]
public class AuthController : ControllerBase {


    [HttpPost]
    [ProducesResponseType(typeof(UserResponseRegisteredAndLoginJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] JsonUserRegistrationRequest request
    ){

        var resultado = await useCase.Executar(request);

        return Created(string.Empty, resultado);

    }


}
