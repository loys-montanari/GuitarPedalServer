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

        public async Task<Curtida> getByUsuarioAndPost(int iduser, int postid)
        {
            var curtida = context.Curtida.Where(x => x.usuarioid == iduser && x.postagemid == postid).FirstOrDefault();
            return curtida;
        }

        public async Task<List<Curtida>> getAllByPost(int idpost)
        {
            var curtida = context.Curtida.Where(x => x.postagemid == idpost).ToList();
            return curtida;
        }

        public async Task<List<Curtida>> getAllByPostTipo(int idpost, int tipo)
        {
            var curtida = context.Curtida.Where(x => x.postagemid == idpost && x.tipo == tipo).ToList();
            return curtida;
        }

        public async Task<List<Curtida>> getAllByPostCurtida(int idpost)
        {
            var curtida = context.Curtida.Where(x => x.postagemid == idpost && x.tipo == 1).ToList();
            return curtida;
        }

        public async Task<List<Curtida>> getAllByPostNaoCurtida(int idpost)
        {
            var curtida = context.Curtida.Where(x => x.postagemid == idpost && x.tipo == 2).ToList();
            return curtida;
        }

        public async Task<Curtida?> getCurtida(Curtida curtida)
        {
            var curt = context.Curtida.Where(x => x.usuarioid == curtida.usuarioid && x.postagemid  == curtida.postagemid).FirstOrDefault();
            return curt;
        }
    }
}
