using Api.Filters.UserLogged;
using Application.UseCases.Product.AddProduct;
using Application.UseCases.Product.GetById;
using Application.UseCases.Product.GetProducts;
using Communication.Requests;
using Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase {

    [HttpPost]
    [ProducesResponseType(typeof(ResponseProductJson), StatusCodes.Status201Created)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> AddProduct(
        [FromBody] RequestProductJson request,
        [FromServices] IAddProductUseCase useCase
    ){
        var resposta = await useCase.Executar(request);

        return Created(string.Empty, resposta);
    }


    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseProductJson>), StatusCodes.Status200OK)]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> GetProducts(
        [FromServices] IGetProductsUseCase useCase
    )
    {
        var resposta = await useCase.Executar();

        return Ok(resposta);
    }


    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseProductJson), StatusCodes.Status200OK)]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> GetProductById(
        [FromServices] IGetProductByIdUseCase useCase,
        [FromRoute] long id
    )
    {
        var resposta = await useCase.Executar(id);

        return Ok(resposta);
    }

}
