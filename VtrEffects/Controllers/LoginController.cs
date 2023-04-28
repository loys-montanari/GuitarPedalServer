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
    public class LoginController    : ControllerBase
    {
        private IUsuarioRepository usuarioRep;
        private ISeguidoresRepository seguidoresRep;

        public LoginController(IUsuarioRepository userrepository, ISeguidoresRepository seguidoresrepository)
        {
            usuarioRep = userrepository;
            seguidoresRep = seguidoresrepository;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO logininfo)
        {
            var user = usuarioRep.GetByEmail(logininfo.email);
            if(user.Result == null)
                return BadRequest("Não existe nenhum cadastro para esse e-mail.");



            string senha = CriptoHelper.HashMD5(logininfo.senha); 
            var ret = usuarioRep.ValidarUsuario(logininfo.email,senha);
            if (ret.Result == null)
            {
                return BadRequest("Senha incorreta.");
            }
            else
            {
                UsuarioInfoDTO retorno = new UsuarioInfoDTO();
                retorno.usuario = usuarioRep.GetByEmail(logininfo.email).Result;
               
                retorno.seguidores = seguidoresRep.GetAllByUsuarioAsync(retorno.usuario.id).Result;

                retorno.seguindo = seguidoresRep.GetAllSeguidosAsync(retorno.usuario.id).Result;


                return Ok(retorno);
            }

        }
    
    
    
    
    }
}
