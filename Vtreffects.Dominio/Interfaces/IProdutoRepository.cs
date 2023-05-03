
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IProdutoRepository : IGenericRepository<Produto>
    {
        Task<Produto> GetBySerial(string serial);
    }
}
