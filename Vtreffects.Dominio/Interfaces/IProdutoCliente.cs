
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IProdutoClienteRepository : IGenericRepository<ProdutoCliente>
    {
        Task<List<ProdutoCliente>> GetAllByUser(int usuarioId);
    }
}
