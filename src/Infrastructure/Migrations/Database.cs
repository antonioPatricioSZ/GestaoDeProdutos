using System.Data.SqlClient;

namespace Infrastructure.Migrations;

public static class Database {

    public static void CreateDatabase(string conexaoComBancoDeDados) {
        
        using var minhaConexao = new SqlConnection(conexaoComBancoDeDados);
       
    }

}
