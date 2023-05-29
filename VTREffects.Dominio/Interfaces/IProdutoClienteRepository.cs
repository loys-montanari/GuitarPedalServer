
using VtrEffects.Dominio.DTO.Generic;
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IProdutoClienteRepository : IGenericRepository<ProdutoCliente>
    {
        Task<bool> VerificarCadastro(string serial);
        Task<ProdutoCliente> GetBySerial(string serial);
        Task<IList<ProdutoCliente>> GetAllByUsuario(int usuarioId);
        Task<IList<Generic2<string, int>>> GetAllSerialTipoProdutoByUsuario(int usuarioId); //<serial, tipoProduto>
    }
}
