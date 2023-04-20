using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class DuvidasRepository : GenericRepository<Duvidas>, IDuvidasRepository
    {
        public DuvidasRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }
    }
}
