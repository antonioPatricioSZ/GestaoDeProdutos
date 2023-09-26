using Application.UseCases.Login;
using Communication.Requests;
using Communication.Responses;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class LoginController : ControllerBase {

    [HttpPost]
    [ProducesResponseType(typeof(UserResponseRegisteredAndLoginJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
        [FromBody] UserLoginJsonRequest request,
        [FromServices] ILoginUseCase useCase
    ){

        var resposta = await useCase.Executar(request);

        return Ok(resposta);

    }

    
}
