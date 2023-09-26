using Microsoft.Extensions.Configuration;

namespace Domain.Extension;

public static class ExtensionRepository {

    public static string GetNameDatabase(this IConfiguration configuration) {
        var nameDatabase = configuration.GetConnectionString("NomeDatabase");
        return nameDatabase;
    }

    public static string GetDatabaseConnection(this IConfiguration configuration) {
        var connectionDatabase = configuration.GetConnectionString("Conexao");
        return connectionDatabase;
    }

}
