﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VtrEffectsDados.Data.Context;

#nullable disable

namespace VtrEffects.Migrations
{
    [DbContext(typeof(ContextVTR))]
    [Migration("20230426211418_NovoDB")]
    partial class NovoDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.AnexoPostagem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<byte[]>("anexo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Anexo");

                    b.Property<string>("extensao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Extensao");

                    b.Property<int>("idPostagem")
                        .HasColumnType("int")
                        .HasColumnName("IdPostagem");

                    b.Property<string>("nomeAnexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NomeAnexo");

                    b.Property<int>("postagemid")
                        .HasColumnType("int");

                    b.Property<int>("tamanho")
                        .HasColumnType("int")
                        .HasColumnName("Tamanho");

                    b.HasKey("id");

                    b.HasIndex("postagemid");

                    b.ToTable("AnexoPostagem");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Comentario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("dataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dataExclusao")
                        .HasColumnType("datetime2");

                    b.Property<int>("idPostagem")
                        .HasColumnType("int")
                        .HasColumnName("IdPostagem");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int")
                        .HasColumnName("IdUsuario");

                    b.Property<int>("postagemid")
                        .HasColumnType("int");

                    b.Property<string>("texto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("usuarioId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("postagemid");

                    b.HasIndex("usuarioId");

                    b.ToTable("Comentario");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Contato", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("linkContato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipoContato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Contato");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Curtida", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("idPostagem")
                        .HasColumnType("int")
                        .HasColumnName("IdPostagem");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int")
                        .HasColumnName("IdUsuario");

                    b.Property<int>("postagemid")
                        .HasColumnType("int");

                    b.Property<int>("tipo")
                        .HasColumnType("int")
                        .HasColumnName("Tipo");

                    b.Property<int>("usuarioId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("postagemid");

                    b.HasIndex("usuarioId");

                    b.ToTable("Curtida");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Duvidas", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("resposta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Duvidas");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Garantia", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("dataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dataFim")
                        .HasColumnType("datetime2");

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("produtoClienteId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Garantia");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Notificacao", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("dataPublicacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("texto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Notificacao");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.NotificacaoUsuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("idNotificacao")
                        .HasColumnType("int");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.Property<bool>("msgLida")
                        .HasColumnType("bit");

                    b.Property<int>("notificacaoid")
                        .HasColumnType("int");

                    b.Property<int>("usuarioId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("notificacaoid");

                    b.HasIndex("usuarioId");

                    b.ToTable("NotificacaoUsuario");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Postagem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<string>("assunto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dataExclusao")
                        .HasColumnType("datetime2");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.Property<string>("texto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Postagem");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Produto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("linkManual")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("linkVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("versaoAtual")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.ProdutoCliente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<bool>("ativo")
                        .HasColumnType("bit");

                    b.Property<int>("idCliente")
                        .HasColumnType("int");

                    b.Property<int>("idProduto")
                        .HasColumnType("int");

                    b.Property<bool>("primeiroComprador")
                        .HasColumnType("bit");

                    b.Property<int>("produtoid")
                        .HasColumnType("int");

                    b.Property<string>("serial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("usuarioId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("produtoid");

                    b.HasIndex("usuarioId");

                    b.ToTable("ProdutoCliente");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentoPrincipal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.AnexoPostagem", b =>
                {
                    b.HasOne("VtrEffects.Dominio.Modelo.Postagem", "postagem")
                        .WithMany()
                        .HasForeignKey("postagemid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("postagem");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Comentario", b =>
                {
                    b.HasOne("VtrEffects.Dominio.Modelo.Postagem", "postagem")
                        .WithMany()
                        .HasForeignKey("postagemid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VtrEffects.Dominio.Modelo.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("postagem");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Curtida", b =>
                {
                    b.HasOne("VtrEffects.Dominio.Modelo.Postagem", "postagem")
                        .WithMany()
                        .HasForeignKey("postagemid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VtrEffects.Dominio.Modelo.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("postagem");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.NotificacaoUsuario", b =>
                {
                    b.HasOne("VtrEffects.Dominio.Modelo.Notificacao", "notificacao")
                        .WithMany()
                        .HasForeignKey("notificacaoid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VtrEffects.Dominio.Modelo.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("notificacao");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.Postagem", b =>
                {
                    b.HasOne("VtrEffects.Dominio.Modelo.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("VtrEffects.Dominio.Modelo.ProdutoCliente", b =>
                {
                    b.HasOne("VtrEffects.Dominio.Modelo.Produto", "produto")
                        .WithMany()
                        .HasForeignKey("produtoid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VtrEffects.Dominio.Modelo.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("produto");

                    b.Navigation("usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
