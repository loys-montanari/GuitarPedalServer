using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class GarantiaRepository : GenericRepository<Garantia>, IGarantiaRepository
    {
        public GarantiaRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }
    }
}
