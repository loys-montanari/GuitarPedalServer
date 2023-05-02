using Microsoft.AspNetCore.Mvc;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private IProdutoClienteRepository produtoClienteRepository;
        private IUsuarioRepository usuarioRepository;
        private IProdutoRepository produtoRepository;
        private ITipoProdutoRepository tipoProdutoRepository;

        public HomeController(IProdutoClienteRepository productoClienteRepository, IUsuarioRepository usuarioRepository, IProdutoRepository produtoRepository, ITipoProdutoRepository tipoProdutoRepository)
        {
            this.produtoClienteRepository = productoClienteRepository;
            this.usuarioRepository = usuarioRepository;
            this.produtoRepository = produtoRepository;
            this.tipoProdutoRepository = tipoProdutoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoById(int produtoId)
        {
            var produto = produtoRepository.GetById(produtoId);
            if (produto is null)
                return BadRequest("Produto não encontrado.");

            return Ok(produto);
        }

        [HttpGet]
        public async Task<ActionResult<IList<TipoProduto>>> GetAllProdutos()
        {
            var produtoList = tipoProdutoRepository.GetAllAsync();
            return Ok(produtoList);
        }

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<IList<ProdutoCliente>>> GetAllProdutosByUsuario(int usuarioId)
        {
            var usuario = usuarioRepository.GetById(usuarioId);
            if (usuario is null)
                return BadRequest("Usuário não encontrado.");

            var produtoClienteList = produtoClienteRepository.GetAllByUsuario(usuarioId);
            return Ok(produtoClienteList);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoCliente>> CadastrarProduto(ProdutoCliente produtoCliente)
        {
            if (produtoCliente is null)
                return BadRequest("Dados para cadastro não encontrados.");

            var cliente = usuarioRepository.GetById(produtoCliente.clienteid);
            if (cliente is null)
                return BadRequest("Código de usuário inválido.");

            var produto = produtoRepository.GetById(produtoCliente.produtoid);
            if (produto is null)
                return BadRequest("Código do produto inválido.");

            var verificaCadastro = produtoClienteRepository.VerificarCadastro(produtoCliente.produtoid);

            if (verificaCadastro.Result == false)
                return BadRequest("Produto já cadastrado.");

            await produtoClienteRepository.SaveAsync(produtoCliente);
            return new CreatedAtRouteResult("GetProdutoByÌd", new { id = produtoCliente.produtoid }, produtoCliente);
        }
    }
}
