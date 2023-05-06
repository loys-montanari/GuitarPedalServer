
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IPostagemRepository : IGenericRepository<Postagem>
    {

        Task<List<Postagem>> getAllNotDeleted();
        Task<int> QtdByUsuario(int usuarioId);
    }
}
