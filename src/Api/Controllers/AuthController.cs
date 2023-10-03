using Application.UseCases.User.Register;
using Communication.Requests;
using Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
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
