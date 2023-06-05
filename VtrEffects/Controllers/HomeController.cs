using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Security.Claims;
using VtrEffects.Caching;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffects.DTO;
using VtrEffectsDados.Data.Context;

namespace VtrEffects.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private IProdutoClienteRepository produtoClienteRepository;
        private IUsuarioRepository usuarioRepository;
        private IProdutoRepository produtoRepository;
        private ITipoProdutoRepository tipoProdutoRepository;
        private ITipoProdutoImagemRepository tipoProdutoImagemRepository;
        private ICachingService _cache;

        public HomeController(ICachingService cache, IProdutoClienteRepository productoClienteRepository, IUsuarioRepository usuarioRepository, IProdutoRepository produtoRepository, ITipoProdutoRepository tipoProdutoRepository, ITipoProdutoImagemRepository tipoProdutoImagemRepository)
        {
            this.produtoClienteRepository = productoClienteRepository;
            this.usuarioRepository = usuarioRepository;
            this.produtoRepository = produtoRepository;
            this.tipoProdutoRepository = tipoProdutoRepository;
            this.tipoProdutoImagemRepository = tipoProdutoImagemRepository;
            this._cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{tipoProdutoId}")]
        public async Task<ActionResult<TipoProdutoDTO>> GetProdutoById(int tipoProdutoId)
        {
            var produtoCache = await _cache.GetAsync($"tipoProduto-{tipoProdutoId}");
            TipoProdutoDTO? tipoProdutoDTO;

            if (!string.IsNullOrEmpty(produtoCache))
            {
                tipoProdutoDTO = JsonConvert.DeserializeObject<TipoProdutoDTO>(produtoCache);
            }
            else
            {
                var tipoProduto = tipoProdutoRepository.GetById(tipoProdutoId);
                if (tipoProduto is null)
                    return BadRequest("Produto não encontrado.");

                tipoProdutoDTO = new TipoProdutoDTO();
                tipoProdutoDTO.produto = tipoProduto;

                var fotoCatalogo = tipoProdutoImagemRepository.GetByTipoProdutoAndTipoImagem(tipoProduto.id, 1).Result.imagem;
                var fotoPng = tipoProdutoImagemRepository.GetByTipoProdutoAndTipoImagem(tipoProduto.id, 2).Result.imagem;

                tipoProdutoDTO.fotoCatalogo = fotoCatalogo;
                tipoProdutoDTO.fotoPng = fotoPng != null ? fotoPng : fotoCatalogo;
            }

            await _cache.SetAsync($"tipoProduto-{tipoProdutoId}", JsonConvert.SerializeObject(tipoProdutoDTO));
            return Ok(tipoProdutoDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IList<TipoProdutoDTO>>> GetAllProdutos()
        {

            var produtosCache = await _cache.GetAsync("Produtos");

            IList<TipoProdutoDTO>? produtosDto;
            if (!string.IsNullOrWhiteSpace(produtosCache))
            {

                produtosDto = JsonConvert.DeserializeObject<IList<TipoProdutoDTO>>(produtosCache);

            }
            else
            {
                produtosDto = new List<TipoProdutoDTO>();
                var produtos = await tipoProdutoRepository.GetAllAsync();

                foreach (TipoProduto produto in produtos)
                {
                    TipoProdutoDTO produtoDTO = new TipoProdutoDTO();
                    produtoDTO.produto = produto;

                    var fotoCatalogo = tipoProdutoImagemRepository.GetByTipoProdutoAndTipoImagem(produto.id, 1).Result.imagem;
                    var fotoPng = tipoProdutoImagemRepository.GetByTipoProdutoAndTipoImagem(produto.id, 2).Result.imagem;

                    produtoDTO.fotoCatalogo = fotoCatalogo;
                    produtoDTO.fotoPng = fotoPng != null ? fotoPng : fotoCatalogo;

                    produtosDto.Add(produtoDTO);
                }

                if (produtos == null)
                    return BadRequest("Nenhum produto encontrado");
            }

            await _cache.SetAsync("Produtos", JsonConvert.SerializeObject(produtosDto));
            return Ok(produtosDto);
        }

        [HttpGet("ProdutosUsuario/{usuarioId}")]
        public async Task<ActionResult<IList<ProdutoDTO>>> GetAllProdutosByUsuario(int usuarioId)
        {
            bool usuario = usuarioRepository.UsuarioExisteById(usuarioId).Result; //Consulta criada apenas para verificar existência de usuário, mais leve em relação à GetByUserId
            if (!usuario)
                return BadRequest("Usuário não encontrado.");

            //var email = User.Claims.Single(x => x.Type == ClaimTypes.Name).Value; //pegando usuario logado

            var produtosCache = await _cache.GetAsync($"produtosUsuario-{usuarioId}");
            IList<ProdutoDTO> prods = new List<ProdutoDTO>();

            if (!string.IsNullOrEmpty(produtosCache))
            {
                prods = JsonConvert.DeserializeObject<IList<ProdutoDTO>>(produtosCache);
            }
            else
            {
                var produtosUsuario = produtoClienteRepository.GetAllSerialTipoProdutoByUsuario(usuarioId).Result; //Consulta mais leve apenas para verificar os produtos do usuário

                foreach (var prodUsuario in produtosUsuario)
                {
                    var getProduto = await GetProdutoById(prodUsuario.prop2);
                    var getProdutoResult = (OkObjectResult)getProduto.Result;
                    var tipoProdutoDTO = (TipoProdutoDTO)getProdutoResult.Value;

                    ProdutoDTO produtoDTO = new ProdutoDTO();
                    produtoDTO.serial = prodUsuario.prop1;
                    produtoDTO.nome = tipoProdutoDTO.produto.nome;
                    produtoDTO.descricao = tipoProdutoDTO.produto.descricao;
                    produtoDTO.fotoProduto = tipoProdutoDTO.fotoCatalogo;
                    produtoDTO.fotoPng = tipoProdutoDTO.fotoPng;

                    prods.Add(produtoDTO);
                }
            }

            await _cache.SetAsync($"produtosUsuario-{usuarioId}", JsonConvert.SerializeObject(prods));
            return Ok(prods);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> CadastrarProduto(ProdutoCadastroDTO produtoCadastro)
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

            ProdutoDTO produtoDTO = new ProdutoDTO();
            produtoDTO.serial = produto.serial;
            produtoDTO.nome = produto.tipoProduto.nome;
            produtoDTO.descricao = produto.tipoProduto.descricao;

            //Caso o usuário tenha seus produtos no cache, adiciona produto que acabou de ser cadastrado
            var produtosCache = await _cache.GetAsync($"produtosUsuario-{usuario.id}");
            IList<ProdutoDTO> prods = new List<ProdutoDTO>();

            if (!string.IsNullOrEmpty(produtosCache))
            {
                prods = JsonConvert.DeserializeObject<IList<ProdutoDTO>>(produtosCache);

                var fotoCatalogo = tipoProdutoImagemRepository.GetByTipoProdutoAndTipoImagem(produto.tipoProduto.id, 1).Result.imagem;
                produtoDTO.fotoProduto = fotoCatalogo;

                prods.Add(produtoDTO); //Adiciona o produto cadastrado na lista de produtos do usuário no cache.
                
            }

            await _cache.SetAsync($"produtosUsuario-{usuario.id}", JsonConvert.SerializeObject(prods));
            return Ok(produtoDTO);
        }

        [HttpPost("TransferenciaProduto")]
        public async Task<ActionResult<ProdutoDTO>> TransferirProduto(TransferenciaDTO transferenciaDTO)
        {
            if (transferenciaDTO is null)
                return BadRequest("Dados para transferência não encontrados.");

            bool usuarioExiste = usuarioRepository.UsuarioExisteById(transferenciaDTO.idUsuarioOrigem).Result;
            if (!usuarioExiste)
                return BadRequest("Usuário de origem não encontrado.");

            usuarioExiste = usuarioRepository.GetByEmail(transferenciaDTO.emailUsuarioDestino).Result == null?  false  : true; 
            if (!usuarioExiste)
                return BadRequest("Usuário de destino não encontrado.");

            var produto = produtoRepository.GetBySerial(transferenciaDTO.serial).Result;
            if (produto is null)
                return BadRequest("Produto não encontrado.");

            ProdutoCliente produtoCliente = produtoClienteRepository.GetBySerial(produto.serial).Result;
            produtoCliente.ativo = false;
            await produtoClienteRepository.UpdateAsync(produtoCliente);

            var usuarioDestino = usuarioRepository.GetByEmail(transferenciaDTO.emailUsuarioDestino).Result;
            ProdutoCliente produtoClienteNovo = new ProdutoCliente();
            produtoClienteNovo.produtoid = produtoCliente.produtoid;
            produtoClienteNovo.produto = produtoCliente.produto;
            produtoClienteNovo.usuarioid = usuarioDestino.id;
            produtoClienteNovo.usuario = usuarioDestino;
            produtoClienteNovo.ativo = true;
            produtoClienteNovo.primeiroComprador = false;
            await produtoClienteRepository.SaveAsync(produtoClienteNovo);

            ProdutoDTO produtoDTO = new ProdutoDTO();
            produtoDTO.serial = produto.serial;
            produtoDTO.nome = produto.tipoProduto.nome;
            produtoDTO.descricao = produto.tipoProduto.descricao;
            produtoDTO.fotoProduto = tipoProdutoImagemRepository.GetByTipoProdutoAndTipoImagem(produto.tipoProduto.id, 1).Result.imagem;

            //Caso o usuário de origem tenha seus produtos no cache, remove o produto que acabou de ser transferido
            var produtosUsuarioOrigem = await _cache.GetAsync($"produtosUsuario-{transferenciaDTO.idUsuarioOrigem}");
            IList<ProdutoDTO> prodsUsuarioOrigem = new List<ProdutoDTO>();
            if (!string.IsNullOrEmpty(produtosUsuarioOrigem))
            {
                prodsUsuarioOrigem = JsonConvert.DeserializeObject<IList<ProdutoDTO>>(produtosUsuarioOrigem);
                prodsUsuarioOrigem = prodsUsuarioOrigem.Where(p => p.serial != produtoDTO.serial).ToList(); //Remove o produto cadastrado na lista de produtos do usuário no cache.
            }
            await _cache.SetAsync($"produtosUsuario-{transferenciaDTO.idUsuarioOrigem}", JsonConvert.SerializeObject(prodsUsuarioOrigem));

            //Caso o usuário de destino tenha seus produtos no cache, adiciona o produto que acabou de ser transferido
            var produtosUsuarioDestino = await _cache.GetAsync($"produtosUsuario-{usuarioDestino.id}");
            IList<ProdutoDTO> prodsUsuarioDestino = new List<ProdutoDTO>();
            if (!string.IsNullOrEmpty(produtosUsuarioDestino))
            {
                prodsUsuarioDestino = JsonConvert.DeserializeObject<IList<ProdutoDTO>>(produtosUsuarioDestino);
                prodsUsuarioDestino.Add(produtoDTO); //Adiciona o produto cadastrado na lista de produtos do usuário no cache.
            }
            await _cache.SetAsync($"produtosUsuario-{usuarioDestino.id}", JsonConvert.SerializeObject(prodsUsuarioDestino));

            return Ok(produtoDTO);
        }
    }
}
