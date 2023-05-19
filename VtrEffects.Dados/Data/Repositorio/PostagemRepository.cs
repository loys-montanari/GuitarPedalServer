using Microsoft.EntityFrameworkCore;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class PostagemRepository : GenericRepository<Postagem>, IPostagemRepository
    {
       
        public PostagemRepository(ContextVTR contextoBI) : base(contextoBI)
        {
            
        }

        public async Task<List<Postagem>> getAllNotDeleted()
        {
            return await entity_.Include("usuario").Where(x => x.dataExclusao == null).ToListAsync();
        }

        public async Task<List<Postagem>?> getAllNotDeletedByUser(int userid)
        {
            return await entity_.Include("usuario").Where(x => x.dataExclusao == null && x.usuarioid == userid).ToListAsync();
        }

        public async Task<int> QtdByUsuario(int usuarioId)
        {
            var posts = await entity_.Where(x => x.usuarioid == usuarioId && x.dataExclusao == null).ToListAsync();
            return posts.Count();
        }

        public async Task<int> QtdPosts()
        {
            var posts = await entity_.Where(x => x.dataExclusao == null).ToListAsync();
            return posts.Count();
        }

    }
}
