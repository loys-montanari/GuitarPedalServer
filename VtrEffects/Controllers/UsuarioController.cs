using Microsoft.AspNetCore.Mvc;
using VtrEffectsDados.Data.Repositorio;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VtrEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {



        private IUsuarioRepository usuarioRep;


        public UsuarioController(IUsuarioRepository userrepository)
        {
            usuarioRep = userrepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<Usuario>> GetbyEmail(string email)
        {
            var user = usuarioRep.GetByEmail(email);
            if (user == null)
                return BadRequest("Usuario não encontrada.");
            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<List<Usuario>>> AddUsuario(Usuario user)
        {
            var usuario = usuarioRep.GetByEmail(user.Email);
            if(usuario != null)
                return Conflict("E-mail já cadastrado.");

            await usuarioRep.SaveAsync(user);

            return Ok(usuarioRep.GetAll());
        }


        [HttpPut]
        public async Task<ActionResult<List<Usuario>>> UpdateUser(Usuario user)
        {
            var usuario = user;
            if (usuario == null)
                return BadRequest("Usuario não encontrada.");

            await usuarioRep.UpdateAsync(user);

            return Ok(usuarioRep.GetByEmail(usuario.Email));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            var user = usuarioRep.GetById(id);
            if (user == null)
                return BadRequest("Usuário não encontrada.");

            await usuarioRep.DeleteAsync(user);
            return Ok(usuarioRep.GetAll());
        }



    }
}
