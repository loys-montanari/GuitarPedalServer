using Microsoft.EntityFrameworkCore;
using VtrEffects.Data.Models;
using VtrEffects.Models;


namespace VtrEffects.Data.Context
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




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("server=localhost;database=vtreffects;Integrated Security=true;TrustServerCertificate=True;");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

            }
        


    }
}
