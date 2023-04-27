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
    }
}
