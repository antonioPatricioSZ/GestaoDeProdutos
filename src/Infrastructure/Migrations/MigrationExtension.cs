using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Migrations;

public static class MigrationExtension {

    public static void MigrateBancoDeDados(this IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();

        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();

        runner.MigrateUp();
    }


    // Este método MigrateBancoDeDados é uma extensão para o tipo IApplicationBuilder e
    // é projetado para ser usado em um pipeline de middleware no ASP.NET Core.Ele 
    // realizamigrações de banco de dados usando o FluentMigrator, que é uma ferramenta
    // de banco de dados para.NET. Aqui está o que o método faz em detalhes:
    // using var scope = app.ApplicationServices.CreateScope();: Cria um escopo de
    // serviço para garantir que todos os serviços necessários sejam criados e
    // gerenciados corretamente durante a execução deste método.
    // var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();:
    // Obtém uma instância do IMigrationRunner dos serviços.O IMigrationRunner é uma
    // parte do FluentMigrator e é usado para executar migrações de banco de dados.
    // runner.ListMigrations();: Lista as migrações disponíveis.Isso pode ser útil
    // para depuração ou para verificar quais migrações já foram aplicadas ao banco
    // de dados. runner.MigrateUp();: Executa migrações ascendentes.Isso aplica todas
    // as migrações pendentes para atualizar o banco de dados para a versão mais
    // recente.As migrações ascendentes geralmente consistem em criar ou modificar
    // tabelas, índices, procedimentos armazenados, etc., de acordo com as alterações
    // no esquema do banco de dados. Em resumo, este método é usado para automatizar
    // o processo de migração do banco de dados ao iniciar a aplicação.Ele cria um
    // escopo de serviço, obtém o IMigrationRunner e, em seguida, executa as migrações
    // pendentes para garantir que o esquema do banco de dados esteja atualizado
    // com as últimas alterações definidas nas migrações.Isso é especialmente útil
    // ao implantar uma aplicação em um ambiente novo ou atualizado, onde o banco
    // de dados precisa ser configurado ou atualizado para refletir as mudanças no
    // modelo de dados.

}
