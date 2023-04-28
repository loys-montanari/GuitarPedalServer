
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IProdutoClienteRepository : IGenericRepository<ProdutoCliente>
    {
        Task<bool> VerificarCadastro(int produtoId);
        Task<IList<ProdutoCliente>> GetAllByUsuario(int usuarioId);
    }
}
