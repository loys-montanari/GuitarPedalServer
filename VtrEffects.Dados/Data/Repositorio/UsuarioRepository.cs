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
            var usuarioDb=     context.Usuario.Where(x => x.Nome.Equals(usuario.Nome) && x.Email.Equals(usuario.Email));
            return usuarioDb.Any();
        }


        public async Task<bool> UsuarioExisteByEmail(string email)
        {
            var usuarioDb = context.Usuario.Where(x => x.Email.Equals(email));
            return usuarioDb.Any();
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            var usuarioDb = context.Usuario.Where(x => x.Email == email).FirstOrDefault();
            return usuarioDb;
        }


        public async Task<Usuario> ValidarUsuario(string email, string senha)
        {

            var usuarioDb = context.Usuario.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();
            return usuarioDb;
        }


    }

}
