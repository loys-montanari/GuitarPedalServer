using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class NotificacaoUsuarioRepository : GenericRepository<NotificacaoUsuario>, INotificacaoUsuarioRepository
    {
        public NotificacaoUsuarioRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }
    }
}
