using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class TipoProdutoRepository : GenericRepository<TipoProduto>, ITipoProdutoRepository
    {
        public TipoProdutoRepository(ContextVTR contextoBI) : base(contextoBI)
        {
            
        }
        public async Task<List<int>> GetAllIds()
        {
            return context.TipoProduto
                    .OrderBy(t => t.id)
                    .Select(t => new { t.id })
                    .ToList()
                    .Select(t => t.id)
                    .ToList();
        }
    }
}
