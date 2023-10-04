using Application.Services.Token;
using Application.Services.UserLogged;
using Application.UseCases.Category.GetAll;
using Application.UseCases.Category.GetById;
using Application.UseCases.Category.Register;
using Application.UseCases.Login;
using Application.UseCases.Product.AddProduct;
using Application.UseCases.Product.DeleteProduct;
using Application.UseCases.Product.GetById;
using Application.UseCases.Product.GetProducts;
using Application.UseCases.User.Register;
using Domain.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Initializer  {
 
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration) {
        AddTokenJwt(configuration, services);
        AddUseCases(services);
        AddUserLogged(services);
        AddConnectionStringAzure(configuration, services);
    }

    private static void AddUserLogged(IServiceCollection services) {
        services.AddScoped<IUserLogged, UserLogged>();
    }


    private static void AddConnectionStringAzure(IConfiguration configuration, IServiceCollection services) {

        var stringConnectionAzure = configuration.GetRequiredSection("Configuracoes:Azure:AzureStorageConnectionString");

        services.AddScoped(options => new AzureStorageStringConnection(
                stringConnectionAzure.Value
            )
        );

    }


    private static void AddTokenJwt(IConfiguration configuration, IServiceCollection services) {

        var tempoDeVidaToken = configuration.GetRequiredSection("Configuracoes:Jwt:TempoVidaTokenMinutos");
        var chaveToken = configuration.GetRequiredSection("Configuracoes:Jwt:ChaveToken");

        services.AddScoped(options => new TokenController(
                int.Parse(tempoDeVidaToken.Value), chaveToken.Value
            )
        ); 

        // Em resumo, o código está configurando a injeção de dependência para a 
        // classe TokenController com um ciclo de vida "scoped".Quando algo 
        // no código solicitar uma instância de TokenController, o contêiner 
        // de injeção de dependência criará uma instância e a fornecerá, 
        // garantindo que os valores corretos(tempoDeVidaToken e chaveToken)
        // sejam passados para o construtor.Isso ajuda a garantir a reutilização
        // e o gerenciamento adequado das dependências em seu aplicativo.NET Core.

    }


    private static void AddUseCases(IServiceCollection service) {
        service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
            .AddScoped<ILoginUseCase, LoginUseCase>()
            .AddScoped<IAddProductUseCase, AddProductUseCase>()
            .AddScoped<IGetProductsUseCase, GetProductsUseCase>()
            .AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>()
            .AddScoped<IRegisterCategoryUseCase, RegisterCategoryUseCase>()
            .AddScoped<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>()
            .AddScoped<IGetCategoryByIdUseCase, GetCategoryByIdUseCase>()
            .AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
    }

}
