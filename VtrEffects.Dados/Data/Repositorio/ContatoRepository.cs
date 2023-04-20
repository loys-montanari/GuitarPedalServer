using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class ContatoRepository : GenericRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }
    }
}
