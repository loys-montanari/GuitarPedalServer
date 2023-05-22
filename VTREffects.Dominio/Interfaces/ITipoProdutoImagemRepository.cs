
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface ITipoProdutoImagemRepository : IGenericRepository<TipoProdutoImagem>
    {
        Task<IList<TipoProdutoImagem>> GetAllByTipoProduto(int tipoProdutoId);
    }
}
