using FluentMigrator;

namespace Infrastructure.Migrations.Versions;

[Migration(
    (long)NumberVersions.CreateTableProductAndCategory,
    "Cria tabela produtos e categorias")
]
public class Version0000002 : Migration {


    public override void Down()
    {}


    public override void Up() {
        CreateTableCategories();
        CreateTableProducts();
    }


    private void CreateTableCategories() {
        var tabela = VersionBase.InsertStandardColumns(Create.Table("Categories"));

        tabela
            .WithColumn("CategoryType").AsInt16().NotNullable()
            .WithColumn("Description").AsString(1500).NotNullable();

    }

    private void CreateTableProducts() {
        var tabela = VersionBase.InsertStandardColumns(Create.Table("Products"));

        tabela
            .WithColumn("Name").AsString(300).NotNullable()
            .WithColumn("Description").AsString(1500).NotNullable()
            .WithColumn("PricePurchase").AsDecimal(10, 2).NotNullable()
            .WithColumn("PriceSale").AsDecimal(10, 2).NotNullable()
            .WithColumn("CategoryId").AsInt64().NotNullable()
                .ForeignKey("FK_Product_Category_Id", "Categories", "Id")
                .OnDeleteOrUpdate(System.Data.Rule.Cascade);
            // Ao exluir uma categoria os produtos que tem o id dessa categoria são exluidos
    }

}
