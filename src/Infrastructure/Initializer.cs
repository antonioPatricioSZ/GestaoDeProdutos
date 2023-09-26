using System.Reflection;
using Domain.Extension;
using Domain.Repositories;
using Domain.Repositories.User;
using FluentMigrator.Runner;
using Infrastructure.AccessRepository;
using Infrastructure.AccessRepository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Initializer {

    public static void AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration
    ){
        AddFluentMigrator(services, configuration);

        AddContext(services, configuration);
        AddUnitOfWork(services);
        AddRepositories(services);
    }


    private static void AddUnitOfWork(IServiceCollection services) {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddRepositories(IServiceCollection services) {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>()
            .AddScoped<IUserReadOnlyRepository, UserRepository>();
    }

    private static void AddContext(
        IServiceCollection services,
        IConfiguration configuration
    ){
        var connectionString = configuration.GetDatabaseConnection();

        services.AddDbContext<GestaoDeProdutosContext>(
            dbContextOptions => dbContextOptions.UseSqlServer(connectionString)
        );
    } 

    private static void AddFluentMigrator(
        IServiceCollection services,
        IConfiguration configuration
    ){
        services.AddFluentMigratorCore().ConfigureRunner(c => 
            c
            .AddSqlServer()
            .WithGlobalConnectionString(configuration.GetDatabaseConnection())
            .ScanIn(Assembly.Load("Infrastructure")).For.All()
        );
    }

}
