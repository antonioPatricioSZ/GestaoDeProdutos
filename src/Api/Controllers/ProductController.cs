using Application.UseCases.Product.AddProduct;
using Application.UseCases.Product.DeleteProduct;
using Application.UseCases.Product.GetById;
using Application.UseCases.Product.GetProducts;
using Application.UseCases.Product.UpdateProduct;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories.Email.SendEmail;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/")]
public class ProductController : ControllerBase {

    [HttpPost]
    [Route("addProduct")]
    [ProducesResponseType(typeof(ResponseProductJson), StatusCodes.Status201Created)]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> AddProduct(
        [FromBody] RequestProductJson request,
        [FromServices] IAddProductUseCase useCase
    )
    {
        var resposta = await useCase.Executar(request);

        return Created(string.Empty, resposta);
    }

    public record Upload(string image);


    [HttpPost]
    [Route("addImage")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public string AddImage(
        [FromBody] Upload model,
        [FromServices] IGetProductsUseCase useCase
    ){
        var resposta = useCase.UploadBase65Image(model.image, "user-images");

        return resposta;
    }


    [HttpGet]
    [Route("getAll")]
    [ProducesResponseType(typeof(List<ResponseProductJson>), StatusCodes.Status200OK)]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> GetProducts(
        [FromServices] IGetProductsUseCase useCase
    ){
        var resposta = await useCase.Executar();

        return Ok(resposta);
    }


    [HttpGet]
    [Route("get/{id}")]
    [ProducesResponseType(typeof(ResponseProductJson), StatusCodes.Status200OK)]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> GetProductById(
        [FromServices] IGetProductByIdUseCase useCase,
        [FromRoute] long id
    ){
        var resposta = await useCase.Executar(id);

        return Ok(resposta);
    }


    [HttpDelete]
    [Route("deletar/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProduct(
        [FromRoute] long id,
        IDeleteProductUseCase useCase
    ){

        await useCase.Executar(id);

        return NoContent();

    }


    [HttpPut]
    [Route("atualizar/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateProduct(
        [FromServices] IUpdateProductUseCase useCase,
        [FromBody] RequestProductJson request,
        [FromRoute] long id
    ){
        await useCase.Executar(id, request);

        return NoContent();
    }


    [HttpGet]
    [Route("teste")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public string TesteApi() {

        return "Tudo certo!";
    }



    [HttpPost]
    [Route("testesendemail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public string TesteEnvioEmail(
        [FromBody] BodySendEmail email,
        [FromServices] ISendEmail sendEmail
    ){
        var imageUrl = "https://testedeployazurestorage.blob.core.windows.net/user-images/1ffd4197-9d13-45e7-9c9f-6641c9131722.jpg";
        sendEmail.Send(
            emailsTo: email.Email,
            subject: "E-mail para recuperação de senha",
        message: $"<html><body><h1>Envio de e-mail teste c#</h1><br><img src=\"{imageUrl}\" alt=\"Azure Blob Image\" /></body></html>"
        );
        return string.Empty;
    }

}
