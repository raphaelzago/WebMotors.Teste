using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMotors.Infra.Migrations
{
    public partial class VersaoVarchar100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "versao",
                table: "tb_AnuncioWebmotors",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "versao",
                table: "tb_AnuncioWebmotors",
                type: "varchar(45)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }
    }
}
