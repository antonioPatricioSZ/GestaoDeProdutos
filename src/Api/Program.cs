using Api.Filters;
using Application;
using Application.Services.AutoMapper;
using Domain.Extension;
using Infrastructure;
using Infrastructure.Migrations;
using AutoMapper;
using Api.Filters.UserLogged;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Infrastructure.AccessRepository;
using Api.Services.Health;
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
    .AddCheck<ApiHealthCheck>(
        "JokesApiChecks",
        tags: new string[] { "Jokes Api" }
    );
    //.AddDbContextCheck<GestaoDeProdutosContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("PermitirApiRequest");

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.MapHealthChecks("/health", new HealthCheckOptions() {
    //AllowCachingResponses = false, // para não fazer cache e ele sempre verificar se tá tudo ok
    //ResultStatusCodes = {
    //    [HealthStatus.Healthy] = StatusCodes.Status200OK,
    //    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    //},
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


AtualizarBaseDeDados();

app.Run();


void AtualizarBaseDeDados() {

    var conexao = builder.Configuration.GetDatabaseConnection();

    // E ao invés de chamar connection string, nós chamamos uma função nossa. Ou seja, a 
    // gente vai implementar uma função para ser chamada como se fosse uma função 
    // dessa variável Configuration.

    // Todo método que você for fazer para chamar dessa forma de uma variável digamos 
    // que não é sua que não foi você criou. E, no caso de Configuration Manager, é uma 
    // uma classe interna do dotnet. Então a gente chama isso de extensão, ok.

    // Como que seu método? Como ele vai saber que isso aqui é uma classe de extensão, 
    // ou seja. Você vai ter acesso a essa função a partir de uma classe
    // que implementa esse IConfiguration, porque ele usa esse modificador this antes 
    // ok, se você tirar ou não utilizar o this, não vai funcionar. Esse this significa 
    // que esse método é uma extensão e essa variável (configuration de IConfiguration)
    // aqui é exatamente a variável que você está usando para chamar essa função.

    Database.CreateDatabase(conexao);

    app.MigrateBancoDeDados();

}
