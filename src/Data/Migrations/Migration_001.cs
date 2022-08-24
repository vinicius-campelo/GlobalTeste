using FluentMigrator;

namespace Data.Migrations
{
    [Migration(001)]
    public class Migration_001 : Migration
    {

        public override void Up()
        {
            Create.Table("Pessoa")
                .WithColumn("Codigo").AsInt32().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(150).NotNullable()
                .WithColumn("Cpf").AsString(22).NotNullable()
                .WithColumn("Uf").AsString(2).NotNullable()
                .WithColumn("DtNascimento").AsDateTime().NotNullable()
            ;

            //Insert.IntoTable("Pessoa")
                //.Row(new { Codigo = 1, Nome = "Vinicius Miranda Campelo", Cpf = "008.774.341-82", Uf = "DF", DtNascimento = "1980-12-17" });


        }

        public override void Down()
        {
            Delete.Table("Pessoa");
        }
    }
}
