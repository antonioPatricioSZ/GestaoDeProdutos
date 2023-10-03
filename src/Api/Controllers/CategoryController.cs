using Api.Filters.UserLogged;
using Application.UseCases.Category.GetAll;
using Application.UseCases.Category.GetById;
using Application.UseCases.Category.Register;
using Communication.Requests;
using Communication.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class CategoryController : ControllerBase {


    [HttpPost]
    [ProducesResponseType(typeof(ResponseCategoryJson), StatusCodes.Status201Created)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> AddCategory(
        [FromBody] RequestRegisterCategoryJson request,
        [FromServices] IRegisterCategoryUseCase useCase
    ){
        var response = await useCase.Executar(request);

        return Created(string.Empty, response);
    }
    

    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseCategoryJson>), StatusCodes.Status200OK)]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> GetAllCategories(
        [FromServices] IGetAllCategoriesUseCase useCase
    ){
        var response = await useCase.Executar();

        return Ok(response);
    }


    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseCategoryJson), StatusCodes.Status200OK)]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> GetCategoryById(
        [FromServices] IGetCategoryByIdUseCase useCase,
        [FromRoute] long id
    )
    {
        var response = await useCase.Executar(id);

        return Ok(response);
    }


}
