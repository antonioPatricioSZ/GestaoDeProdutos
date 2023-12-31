using Api.Filters;
using Application;
using Application.Services.AutoMapper;
using Domain.Extension;
using Infrastructure;
using Infrastructure.Migrations;
using AutoMapper;
using Api.Filters.UserLogged;
using Api.Services.Health;
using Infrastructure.AccessRepository;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(config => config.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionsFilter)));

builder.Services.AddScoped(provider => new MapperConfiguration(config => {
    config.AddProfile(new AutoMapperConfiguration());
}).CreateMapper());


builder.Services.AddScoped<AuthenticatedUserAttribute>();


builder.Services.AddCors(options => {
    options.AddPolicy(
        name: "PermitirApiRequest",
        build => build.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
    );
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<GestaoDeProdutosContext>()
    .AddCheck<ApiHealthCheck>(
        "JokesApiChecks",
        tags: new string[] { "Jokes Api" }
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirApiRequest");

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.MapHealthChecks("/health", new HealthCheckOptions{
    AllowCachingResponses = false, // para n�o fazer cache e ele sempre verificar se t� tudo ok
    ResultStatusCodes = {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


AtualizarBaseDeDados();

app.Run();


void AtualizarBaseDeDados() {

    var conexao = builder.Configuration.GetDatabaseConnection();

    // E ao inv�s de chamar connection string, n�s chamamos uma fun��o nossa. Ou seja, a 
    // gente vai implementar uma fun��o para ser chamada como se fosse uma fun��o 
    // dessa vari�vel Configuration.

    // Todo m�todo que voc� for fazer para chamar dessa forma de uma vari�vel digamos 
    // que n�o � sua que n�o foi voc� criou. E, no caso de Configuration Manager, � uma 
    // uma classe interna do dotnet. Ent�o a gente chama isso de extens�o, ok.

    // Como que seu m�todo? Como ele vai saber que isso aqui � uma classe de extens�o, 
    // ou seja. Voc� vai ter acesso a essa fun��o a partir de uma classe
    // que implementa esse IConfiguration, porque ele usa esse modificador this antes 
    // ok, se voc� tirar ou n�o utilizar o this, n�o vai funcionar. Esse this significa 
    // que esse m�todo � uma extens�o e essa vari�vel (configuration de IConfiguration)
    // aqui � exatamente a vari�vel que voc� est� usando para chamar essa fun��o.

    Database.CreateDatabase(conexao);

    app.MigrateBancoDeDados();

}
