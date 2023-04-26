using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtrEffects.Migrations
{
    /// <inheritdoc />
    public partial class NovoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoContato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    linkContato = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Duvidas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resposta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duvidas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Garantia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    produtoClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garantia", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Notificacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataPublicacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    linkManual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    linkVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    versaoAtual = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentoPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificacaoUsuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    idNotificacao = table.Column<int>(type: "int", nullable: false),
                    notificacaoid = table.Column<int>(type: "int", nullable: false),
                    msgLida = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacaoUsuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_NotificacaoUsuario_Notificacao_notificacaoid",
                        column: x => x.notificacaoid,
                        principalTable: "Notificacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificacaoUsuario_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Postagem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assunto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataExclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postagem", x => x.id);
                    table.ForeignKey(
                        name: "FK_Postagem_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoCliente",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProduto = table.Column<int>(type: "int", nullable: false),
                    produtoid = table.Column<int>(type: "int", nullable: false),
                    idCliente = table.Column<int>(type: "int", nullable: false),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    serial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    primeiroComprador = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoCliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProdutoCliente_Produto_produtoid",
                        column: x => x.produtoid,
                        principalTable: "Produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoCliente_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnexoPostagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPostagem = table.Column<int>(type: "int", nullable: false),
                    postagemid = table.Column<int>(type: "int", nullable: false),
                    NomeAnexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extensao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamanho = table.Column<int>(type: "int", nullable: false),
                    Anexo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexoPostagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnexoPostagem_Postagem_postagemid",
                        column: x => x.postagemid,
                        principalTable: "Postagem",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataExclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    IdPostagem = table.Column<int>(type: "int", nullable: false),
                    postagemid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comentario_Postagem_postagemid",
                        column: x => x.postagemid,
                        principalTable: "Postagem",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Comentario_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Curtida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    IdPostagem = table.Column<int>(type: "int", nullable: false),
                    postagemid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curtida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curtida_Postagem_postagemid",
                        column: x => x.postagemid,
                        principalTable: "Postagem",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Curtida_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnexoPostagem_postagemid",
                table: "AnexoPostagem",
                column: "postagemid");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_postagemid",
                table: "Comentario",
                column: "postagemid");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_usuarioId",
                table: "Comentario",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Curtida_postagemid",
                table: "Curtida",
                column: "postagemid");

            migrationBuilder.CreateIndex(
                name: "IX_Curtida_usuarioId",
                table: "Curtida",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacaoUsuario_notificacaoid",
                table: "NotificacaoUsuario",
                column: "notificacaoid");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacaoUsuario_usuarioId",
                table: "NotificacaoUsuario",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Postagem_UsuarioId",
                table: "Postagem",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCliente_produtoid",
                table: "ProdutoCliente",
                column: "produtoid");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCliente_usuarioId",
                table: "ProdutoCliente",
                column: "usuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnexoPostagem");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "Curtida");

            migrationBuilder.DropTable(
                name: "Duvidas");

            migrationBuilder.DropTable(
                name: "Garantia");

            migrationBuilder.DropTable(
                name: "NotificacaoUsuario");

            migrationBuilder.DropTable(
                name: "ProdutoCliente");

            migrationBuilder.DropTable(
                name: "Postagem");

            migrationBuilder.DropTable(
                name: "Notificacao");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
