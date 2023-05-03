using Microsoft.AspNetCore.Mvc;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffects.DTO;
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

        [HttpGet("{tipoProdutoId}")]
        public async Task<ActionResult<TipoProduto>> GetProdutoById(int tipoProdutoId)
        {
            var tipoProduto = tipoProdutoRepository.GetById(tipoProdutoId);
            if (tipoProduto is null)
                return BadRequest("Produto não encontrado.");

            return Ok(tipoProduto);
        }

        [HttpGet]
        public async Task<ActionResult<IList<TipoProduto>>> GetAllProdutos()
        {
            var produtoList = tipoProdutoRepository.GetAllAsync().Result;
            return Ok(produtoList);
        }

        [HttpGet("Produtos/{usuarioId}")]
        public async Task<ActionResult<IList<ProdutoCliente>>> GetAllProdutosByUsuario(int usuarioId)
        {
            var usuario = usuarioRepository.GetById(usuarioId);
            if (usuario is null)
                return BadRequest("Usuário não encontrado.");

            var produtoClienteList = produtoClienteRepository.GetAllByUsuario(usuarioId);
            return Ok(produtoClienteList);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoCliente>> CadastrarProduto(ProdutoCadastroDTO produtoCadastro)
        {
            if (produtoCadastro is null)
                return BadRequest("Dados para cadastro não encontrados.");

            var usuario = usuarioRepository.GetById(produtoCadastro.usuarioId);
            if (usuario is null)
                return BadRequest("Código de usuário inválido.");

            var produto = produtoRepository.GetBySerial(produtoCadastro.serial).Result;
            if (produto is null)
                return BadRequest("Código do produto inválido.");

            var verificaCadastro = produtoClienteRepository.VerificarCadastro(produtoCadastro.serial).Result;
            if (verificaCadastro == false)
                return BadRequest("Produto já cadastrado.");

            ProdutoCliente produtoCliente = new ProdutoCliente();
            produtoCliente.produtoid = produto.id;
            produtoCliente.produto = produto;
            produtoCliente.ativo = true;
            produtoCliente.primeiroComprador = true; // O que definir aqui?
            produtoCliente.usuarioid = usuario.id;
            produtoCliente.usuario = usuario;

            await produtoClienteRepository.SaveAsync(produtoCliente);
            return Ok(produtoCliente);
            //return new CreatedAtRouteResult("GetProdutoByÌd", new { id = produtoCliente.produtoid }, produtoCliente);
        }
    }
}
