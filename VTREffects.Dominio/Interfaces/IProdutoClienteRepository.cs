
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IProdutoClienteRepository : IGenericRepository<ProdutoCliente>
    {
        Task<bool> VerificarCadastro(string serial);
        Task<IList<ProdutoCliente>> GetAllByUsuario(int usuarioId);
    }
}
