using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        private readonly ContextVTR context;
        public ProdutoRepository(ContextVTR contextoBI) : base(contextoBI)
        {
            this.context = contextoBI;
        }
    }
}
