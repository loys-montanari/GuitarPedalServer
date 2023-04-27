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
    }
}
