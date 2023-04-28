
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface ISeguidoresRepository : IGenericRepository<Seguidores>
    {

        Task<List<Seguidores>> GetAllByUsuarioAsync(int id);
        Task<List<Seguidores>> GetAllSeguidosAsync(int id);
    }
}
