using Application.Services.Token;
using Application.UseCases.Login;
using Application.UseCases.User.Register;
using Domain.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Initializer  {
 
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration) {
        AddTokenJwt(configuration, services);
        AddUseCases(services);
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
            .AddScoped<ILoginUseCase, LoginUseCase>();
    }

}
