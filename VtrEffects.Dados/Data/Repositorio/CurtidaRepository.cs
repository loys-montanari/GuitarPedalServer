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


        public async Task<Curtida> getByUsuario(int iduser)
        {
            var curtida = context.Curtida.Where(x => x.usuarioid == iduser).FirstOrDefault();
            return curtida;
        }

        public async Task<List<Curtida>> getAllByPost(int idpost)
        {
            var curtida = context.Curtida.Where(x => x.postagemid == idpost).ToList();
            return curtida;
        }
    }
}
