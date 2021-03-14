using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMotors.Infra.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_AnuncioWebmotors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    marca = table.Column<string>(type: "varchar(45)", nullable: false),
                    modelo = table.Column<string>(type: "varchar(45)", nullable: false),
                    versao = table.Column<string>(type: "varchar(45)", nullable: false),
                    ano = table.Column<int>(type: "int", nullable: false),
                    quilometragem = table.Column<int>(type: "int", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_AnuncioWebmotors", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_AnuncioWebmotors");
        }
    }
}
