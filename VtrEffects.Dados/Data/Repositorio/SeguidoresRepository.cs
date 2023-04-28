using Microsoft.EntityFrameworkCore;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class SeguidoresRepository : GenericRepository<Seguidores>, ISeguidoresRepository
    {
        public SeguidoresRepository(ContextVTR contextoBI) : base(contextoBI)
        {
           
        }

        public async Task<List<Seguidores>> GetAllByUsuarioAsync(int id)
        {
            return await entity_.Include(s => s.seguidor).Where(x => x.idUsuario == id).ToListAsync();
        }

        public async Task<List<Seguidores>> GetAllSeguidosAsync(int id)
        {
            return await entity_.Include( s=> s.usuario).Where(x => x.idSeguidor == id).ToListAsync();
        }
    }
}
 