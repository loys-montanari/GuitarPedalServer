using Microsoft.AspNetCore.Mvc;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private IUsuarioRepository usuarioRep;
        private IProdutoClienteRepository produtoClienteRep;

        public HomeController(IUsuarioRepository usuarioRepository, IProdutoClienteRepository produtoClienteRepository)
        {
            this.usuarioRep = usuarioRepository;
            this.produtoClienteRep = produtoClienteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProdutoCliente>>> GetProdutosByUser(int id)
        {
            var usuario = usuarioRep.GetById(id);
            if (usuario == null)
                return BadRequest("Usuário não encontrado.");
            
            var produtos = produtoClienteRep.GetAllByUser(id);
            if (produtos.Result == null || produtos.Result.Count() == 0)
                return BadRequest("Usuário não contém produtos cadastrados.");

            return Ok(produtos);
        }
    }
}
