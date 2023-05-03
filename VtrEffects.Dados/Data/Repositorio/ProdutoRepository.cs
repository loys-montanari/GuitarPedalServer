using Microsoft.EntityFrameworkCore;
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

        public async Task<Produto> GetBySerial(string serial)
        {
            return context.Produto.Include(p => p.tipoProdutoId).FirstOrDefault(p => p.serial == serial);
        }
    }
}
