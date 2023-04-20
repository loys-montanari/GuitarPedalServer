using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class CurtidaRepository : GenericRepository<Curtida>,ICurtidaRepository
    {
        public CurtidaRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }
    }
}
