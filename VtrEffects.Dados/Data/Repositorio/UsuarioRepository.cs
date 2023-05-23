using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;
using Microsoft.EntityFrameworkCore;
using VtrEffectsDados.Data.Repositorio;

namespace VtrEffectsDados.Data.Repositorio
{

  

    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {

        private readonly ContextVTR context;
        public UsuarioRepository(ContextVTR contextoBI) : base(contextoBI)
        {
            this.context = contextoBI;
        }

   

        public async Task<bool> UsuarioExiste(Usuario usuario)
        {
            var usuarioDb=     context.Usuario.Where(x => x.nome.Equals(usuario.nome) && x.email.Equals(usuario.email));
            return usuarioDb.Any();
        }


        public async Task<bool> UsuarioExisteByEmail(string email)
        {
            var usuarioDb = context.Usuario.Where(x => x.email.Equals(email));
            return usuarioDb.Any();
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            var usuarioDb = context.Usuario.Where(x => x.email == email).FirstOrDefault();
            return usuarioDb;
        }

        public async Task<int> GetIdByEmail(string email)
        {
            var usuarioDb = context.Usuario.Where(x => x.email == email).Select(x => x.id).FirstOrDefault();
            return usuarioDb;
        }

        public async Task<Usuario> ValidarUsuario(string email, string senha)
        {

            var usuarioDb = context.Usuario.Where(x => x.email == email && x.senha == senha).FirstOrDefault();
            return usuarioDb;
        }


    }

}
