
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface ICurtidaRepository : IGenericRepository<Curtida>
    {
        Task<Curtida?> getCurtida(Curtida curtida);
        Task<Curtida> getByUsuario(int iduser);
        Task<List<Curtida>> getAllByPost(int idpost);
        Task<List<Curtida>> getAllByPostTipo(int idpost, int tipo);
        Task<Curtida> getByUsuarioAndPost(int iduser, int postid);
    }
}
