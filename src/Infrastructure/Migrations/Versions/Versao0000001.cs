using FluentMigrator;

namespace Infrastructure.Migrations.Versions;


[Migration((long)NumberVersions.CreateTableUser, "Cria tabela de usuarios")]
public class Versao0000001 : Migration {

    public override void Down() 
    {}

    public override void Up() {
        var tabela = VersionBase.InsertStandardColumns(Create.Table("Users"));
        
        tabela
            .WithColumn("Name").AsString(150).NotNullable()
            .WithColumn("Email").AsString(300).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable()
            .WithColumn("PhoneNumber").AsString(14).NotNullable()
            .WithColumn("BirthDate").AsDateTime().NotNullable();

    }

}
