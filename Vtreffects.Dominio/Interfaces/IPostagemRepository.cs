
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IPostagemRepository : IGenericRepository<Postagem>
    {

        Task<List<Postagem>> getAllNotDeleted();
        Task<List<Postagem>?> getAllNotDeletedByUser(int userid);
    }
}
