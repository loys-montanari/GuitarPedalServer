using Microsoft.EntityFrameworkCore;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class ProdutoClienteRepository : GenericRepository<ProdutoCliente>, IProdutoClienteRepository
    {
        private readonly ContextVTR context;

        public ProdutoClienteRepository(ContextVTR contextoBI) : base(contextoBI)
        {
            this.context = contextoBI;
        }

        public async Task<List<ProdutoCliente>> GetAllByUser(int userId)
        {
            var productList = context.ProdutoCliente.Include(p => p.produto).Where(p => p.idCliente == userId).ToList();
            return productList;
        }
    }
}
