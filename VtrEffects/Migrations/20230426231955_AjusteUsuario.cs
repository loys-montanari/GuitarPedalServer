using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtrEffects.Migrations
{
    /// <inheritdoc />
    public partial class AjusteUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Usuario_usuarioId",
                table: "Comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_Curtida_Usuario_usuarioId",
                table: "Curtida");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificacaoUsuario_Usuario_usuarioId",
                table: "NotificacaoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Postagem_Usuario_UsuarioId",
                table: "Postagem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoCliente_Usuario_usuarioId",
                table: "ProdutoCliente");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuario",
                newName: "senha");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Usuario",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuario",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuario",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "usuarioId",
                table: "ProdutoCliente",
                newName: "usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoCliente_usuarioId",
                table: "ProdutoCliente",
                newName: "IX_ProdutoCliente_usuarioid");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Postagem",
                newName: "Usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_Postagem_UsuarioId",
                table: "Postagem",
                newName: "IX_Postagem_Usuarioid");

            migrationBuilder.RenameColumn(
                name: "usuarioId",
                table: "NotificacaoUsuario",
                newName: "usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_NotificacaoUsuario_usuarioId",
                table: "NotificacaoUsuario",
                newName: "IX_NotificacaoUsuario_usuarioid");

            migrationBuilder.RenameColumn(
                name: "usuarioId",
                table: "Curtida",
                newName: "usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_Curtida_usuarioId",
                table: "Curtida",
                newName: "IX_Curtida_usuarioid");

            migrationBuilder.RenameColumn(
                name: "usuarioId",
                table: "Comentario",
                newName: "usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_Comentario_usuarioId",
                table: "Comentario",
                newName: "IX_Comentario_usuarioid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Usuario_usuarioid",
                table: "Comentario",
                column: "usuarioid",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Curtida_Usuario_usuarioid",
                table: "Curtida",
                column: "usuarioid",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificacaoUsuario_Usuario_usuarioid",
                table: "NotificacaoUsuario",
                column: "usuarioid",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagem_Usuario_Usuarioid",
                table: "Postagem",
                column: "Usuarioid",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoCliente_Usuario_usuarioid",
                table: "ProdutoCliente",
                column: "usuarioid",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Usuario_usuarioid",
                table: "Comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_Curtida_Usuario_usuarioid",
                table: "Curtida");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificacaoUsuario_Usuario_usuarioid",
                table: "NotificacaoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Postagem_Usuario_Usuarioid",
                table: "Postagem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoCliente_Usuario_usuarioid",
                table: "ProdutoCliente");

            migrationBuilder.RenameColumn(
                name: "senha",
                table: "Usuario",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Usuario",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Usuario",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Usuario",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "ProdutoCliente",
                newName: "usuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoCliente_usuarioid",
                table: "ProdutoCliente",
                newName: "IX_ProdutoCliente_usuarioId");

            migrationBuilder.RenameColumn(
                name: "Usuarioid",
                table: "Postagem",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Postagem_Usuarioid",
                table: "Postagem",
                newName: "IX_Postagem_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "NotificacaoUsuario",
                newName: "usuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificacaoUsuario_usuarioid",
                table: "NotificacaoUsuario",
                newName: "IX_NotificacaoUsuario_usuarioId");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "Curtida",
                newName: "usuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Curtida_usuarioid",
                table: "Curtida",
                newName: "IX_Curtida_usuarioId");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "Comentario",
                newName: "usuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentario_usuarioid",
                table: "Comentario",
                newName: "IX_Comentario_usuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Usuario_usuarioId",
                table: "Comentario",
                column: "usuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Curtida_Usuario_usuarioId",
                table: "Curtida",
                column: "usuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificacaoUsuario_Usuario_usuarioId",
                table: "NotificacaoUsuario",
                column: "usuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagem_Usuario_UsuarioId",
                table: "Postagem",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoCliente_Usuario_usuarioId",
                table: "ProdutoCliente",
                column: "usuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
