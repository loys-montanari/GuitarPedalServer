
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IComentarioRepository : IGenericRepository<Comentario>
    {

        Task<List<Comentario>> getAllByPost(int id);
    }

}
