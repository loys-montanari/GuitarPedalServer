using Microsoft.EntityFrameworkCore;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class ComentarioRepository : GenericRepository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }


        public async Task<List<Comentario>> getAllByPost(int id)
        {
            return await entity_.Include(s => s.usuario).Where(x => x.postagemid == id && x.dataExclusao == null).ToListAsync();
        }

    }
}
