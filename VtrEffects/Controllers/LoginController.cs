using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VtrEffects.Helpers;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffects.DTO;
using VtrEffectsDados.Data.Repositorio;

namespace VtrEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository usuarioRep;


        public LoginController(IUsuarioRepository userrepository)
        {
            usuarioRep = userrepository;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO logininfo)
        {
            var user = usuarioRep.GetByEmail(logininfo.Email);
            if(user.Result == null)
                return BadRequest("Não existe nenhum cadastro para esse e-mail.");



            string senha = CriptoHelper.HashMD5(logininfo.Password); 
            var ret = usuarioRep.ValidarUsuario(logininfo.Email,senha);
            if (ret.Result == null)
            {
                return BadRequest("Senha incorreta.");
            }
            else
            {
                return Ok(usuarioRep.GetByEmail(logininfo.Email));
            }

        }
    }
}
