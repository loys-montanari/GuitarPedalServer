using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class AnexoRepository : GenericRepository<AnexoPostagem>, IAnexoRepository 
    {
        public AnexoRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }


        public async Task<List<AnexoPostagem>> getAllByPost(int idpost)
        {
            var anexo = context.AnexoPostagem.Where(x => x.postagemid == idpost).ToList();
            return anexo;
        }
    }
}
