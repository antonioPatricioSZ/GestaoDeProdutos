using FluentMigrator;

namespace Infrastructure.Migrations.Versions;

[Migration(
    (long)NumberVersions.AlterTableCategory,
    "Altera na tabela de categoria o tipo da coluna CategoryType e a renomeia")
]

public class Version0000003 : Migration {

    public override void Down()
    {}


    public override void Up() {

        Rename.Column("CategoryType").OnTable("Categories").To("CategoryName"); 
        Alter.Table("Categories").AlterColumn("CategoryName").AsString().NotNullable();

    }

}
