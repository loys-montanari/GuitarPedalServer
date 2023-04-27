using Microsoft.EntityFrameworkCore;
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
        public DbSet<TipoProduto> TipoProduto { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutoCliente> ProdutoCliente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


        public DbSet<Seguidores> Seguidores { get; set; }


        public ContextVTR(DbContextOptions<ContextVTR> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }             

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=tcp:vtreffectshi5.database.windows.net,1433;Initial Catalog=VtrEffects;Persist Security Info=False;User ID=vtradmin;Password=Admin09!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", b => b.MigrationsAssembly("VtrEffects"));
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
