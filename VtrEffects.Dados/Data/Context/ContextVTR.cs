﻿using Microsoft.EntityFrameworkCore;
using System.Configuration;
using VtrEffects.Dominio.Modelo;


namespace VtrEffectsDados.Data.Context
{
    public class ContextVTR : DbContext
    {

        public DbSet<AnexoPostagem> AnexoPostagem { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Curtida> Curtida { get; set; }
        public DbSet<Duvidas> Duvidas { get; set; }
        public DbSet<Garantia> Garantia { get; set; }
        public DbSet<Notificacao> Notificacao { get; set; }
        public DbSet<NotificacaoUsuario> NotificacaoUsuario { get; set; }
        public DbSet<Postagem> Postagem { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutoCliente> ProdutoCliente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


        public ContextVTR(DbContextOptions<ContextVTR> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("server=localhost;database=vtreffects;Integrated Security=true;TrustServerCertificate=True;");
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }



    }
}
