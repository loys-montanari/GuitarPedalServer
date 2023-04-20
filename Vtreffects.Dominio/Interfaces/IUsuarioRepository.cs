
using VtrEffects.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IUsuarioRepository 
    {
        Task<bool> UsuarioExiste(Usuario usuario);
        Task SaveAsync(Usuario usuario);
        Task<IEnumerable<Usuario>> AllAsync();


    }
}
