using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclamacao.Dados.Data.Repositorio
{

  

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IGenericRepository<Usuario> _repositoryBase;
        private readonly ContextVTR context;

        public UsuarioRepository(IGenericRepository<Usuario> repositoryBase, ContextVTR context)
        {
            _repositoryBase = repositoryBase;
            this.context = context;
        }

        public async Task<IEnumerable<Usuario>> AllAsync()
        {
            var lista = await context.Usuario.ToListAsync();
            return lista;
        }

        public async Task SaveAsync(Usuario usuario)
        {
            await _repositoryBase.SaveAsync(usuario);
        }

        public async Task<bool> UsuarioExiste(Usuario usuario)
        {
            var usuarioDb=     context.Usuario.Where(x => x.Nome.Equals(usuario.Nome) && x.Email.Equals(usuario.Email));
            return usuarioDb.Any();
        }
    }

}
