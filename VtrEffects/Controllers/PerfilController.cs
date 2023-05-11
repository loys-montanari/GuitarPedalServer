using Microsoft.AspNetCore.Mvc;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.DTO;

namespace VtrEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : Controller
    {
        private IUsuarioRepository usuarioRep;
        private ISeguidoresRepository seguidoresRep;
        private IPostagemRepository postagemRep;

        public PerfilController(IUsuarioRepository usuarioRepository, ISeguidoresRepository seguidoresRepository, IPostagemRepository postagemRepository)
        {
            usuarioRep = usuarioRepository;
            seguidoresRep = seguidoresRepository;
            postagemRep = postagemRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<PerfilDTO>> GetByUsuario(int usuarioId)
        {
            var usuario = usuarioRep.GetById(usuarioId);
            if (usuario == null)
                return BadRequest();

            PerfilDTO perfil = new PerfilDTO();
            perfil.usuario = usuario;
            perfil.followers = seguidoresRep.GetAllByUsuarioAsync(usuario.id).Result;
            perfil.following = seguidoresRep.GetAllSeguidosAsync(usuario.id).Result;
            perfil.qtdPosts = postagemRep.QtdByUsuario(usuario.id).Result;

            return Ok(perfil);
        }
    }
}
