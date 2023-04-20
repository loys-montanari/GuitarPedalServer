using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class NotificacaoRepository : GenericRepository<Notificacao>, INotificacaoRepository
    {
        public NotificacaoRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }
    }
}
