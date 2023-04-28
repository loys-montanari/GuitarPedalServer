using Microsoft.AspNetCore.Mvc;
using VtrEffectsDados.Data.Repositorio;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;
using VtrEffects.Helpers;
using VtrEffects.DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VtrEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;
        private IUsuarioRepository usuarioRep;
        private ISeguidoresRepository seguidoresRep;


        public UsuarioController(IUsuarioRepository userrepository, ISeguidoresRepository seguidoresrepository)
        {

            usuarioRep = userrepository;
            seguidoresRep = seguidoresrepository;
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


        [HttpPost(), Route("AlterarSenha")]
        public async Task<ActionResult<Usuario>> AlterarSenha(AlterarSenhaDTO infonovasenha)
        {

            var user = usuarioRep.GetById(infonovasenha.idusuario);
            if (user == null)
                return BadRequest("Usuário não encontrada.");

            if(infonovasenha.senhaatual == infonovasenha.novasenha)
                return BadRequest("Escolha uma senha nova diferente da antiga.");

            string senha = CriptoHelper.HashMD5(infonovasenha.senhaatual);
            var ret = usuarioRep.ValidarUsuario(user.email, senha);
            if (ret.Result == null)
            {
                return BadRequest("Senha atual não confere.");
            }

            user.senha = CriptoHelper.HashMD5(infonovasenha.novasenha);
            await usuarioRep.UpdateAsync(user);

            UsuarioInfoDTO retorno = new UsuarioInfoDTO();
            retorno.usuario = usuarioRep.GetByEmail(user.email).Result;

            retorno.seguidores = seguidoresRep.GetAllByUsuarioAsync(retorno.usuario.id).Result;

            retorno.seguindo = seguidoresRep.GetAllSeguidosAsync(retorno.usuario.id).Result;

            return Ok(retorno);
        }

    }
}
