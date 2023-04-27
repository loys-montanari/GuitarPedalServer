using Microsoft.AspNetCore.Mvc;
using VtrEffectsDados.Data.Repositorio;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;
using VtrEffects.Helpers;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VtrEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;
        private IUsuarioRepository usuarioRep;


        public UsuarioController(IUsuarioRepository userrepository)
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(httpClientHandler);
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
            if (user.Result == null)
                return BadRequest("Usuario não encontrada.");
            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<List<Usuario>>> AddUsuario(Usuario user)
        {
            var usuario = usuarioRep.GetByEmail(user.email);
            if(usuario.Result != null)
                return Conflict("E-mail já cadastrado.");

            user.senha = CriptoHelper.HashMD5(user.senha);
            await usuarioRep.SaveAsync(user);

            return Ok(200);
        }


        [HttpPut]
        public async Task<ActionResult<List<Usuario>>> UpdateUser(Usuario user)
        {
            var usuario = user;
            if (usuario == null)
                return BadRequest("Usuario não encontrada.");

            await usuarioRep.UpdateAsync(user);

            return Ok(usuarioRep.GetByEmail(usuario.email));
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
