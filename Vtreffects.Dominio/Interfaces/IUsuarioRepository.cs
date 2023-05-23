
using VtrEffects.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> GetByEmail(string email);

        Task<int> GetIdByEmail(string email);
        Task<bool> UsuarioExisteByEmail(string email);
        Task<bool> UsuarioExiste(Usuario usuario);

        Task<Usuario> ValidarUsuario(string email, string senha);


    }
}
