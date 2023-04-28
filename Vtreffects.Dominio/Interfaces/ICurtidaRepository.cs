
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface ICurtidaRepository : IGenericRepository<Curtida>
    {

         Task<Curtida> getByUsuario(int iduser);
        Task<List<Curtida>> getAllByPost(int idpost);
    }
}
