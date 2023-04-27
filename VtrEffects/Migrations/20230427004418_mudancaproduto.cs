using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtrEffects.Migrations
{
    /// <inheritdoc />
    public partial class mudancaproduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descricao",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "linkManual",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "linkVideo",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "nome",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "InstrumentoPrincipal",
                table: "Usuario",
                newName: "instrumentoPrincipal");

            migrationBuilder.RenameColumn(
                name: "Foto",
                table: "Usuario",
                newName: "foto");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "Usuario",
                newName: "bio");

            migrationBuilder.RenameColumn(
                name: "versaoAtual",
                table: "Produto",
                newName: "serial");

            migrationBuilder.AddColumn<int>(
                name: "idProduto",
                table: "Produto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "produtoid",
                table: "Produto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_produtoid",
                table: "Produto",
                column: "produtoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Produto_produtoid",
                table: "Produto",
                column: "produtoid",
                principalTable: "Produto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Produto_produtoid",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_produtoid",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "idProduto",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "produtoid",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "instrumentoPrincipal",
                table: "Usuario",
                newName: "InstrumentoPrincipal");

            migrationBuilder.RenameColumn(
                name: "foto",
                table: "Usuario",
                newName: "Foto");

            migrationBuilder.RenameColumn(
                name: "bio",
                table: "Usuario",
                newName: "Bio");

            migrationBuilder.RenameColumn(
                name: "serial",
                table: "Produto",
                newName: "versaoAtual");

            migrationBuilder.AddColumn<string>(
                name: "descricao",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "linkManual",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "linkVideo",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nome",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
