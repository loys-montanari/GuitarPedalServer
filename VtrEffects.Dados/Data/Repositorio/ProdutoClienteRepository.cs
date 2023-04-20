using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class ProdutoClienteRepository : GenericRepository<ProdutoCliente>, IProdutoClienteRepository
    {
        public ProdutoClienteRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }
    }
}
