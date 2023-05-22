using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<ActionResult<TipoProduto>> GetProdutoById(int tipoProdutoId)
        {
            var tipoProduto = tipoProdutoRepository.GetById(tipoProdutoId);
            if (tipoProduto is null)
                return BadRequest("Produto não encontrado.");

            return Ok(tipoProduto);
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

                    if (fotoCatalogo != null)
                    {
                        produtoDTO.fotoCatalogo = fotoCatalogo;
                        if (fotoPng != null)
                        {
                            produtoDTO.fotoPng = fotoPng;
                        }
                        else
                        {
                            produtoDTO.fotoPng = fotoCatalogo;
                        }
                    }
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
            var usuario = usuarioRepository.GetById(usuarioId);
            if (usuario is null)
                return BadRequest("Usuário não encontrado.");

            var email = User.Claims.Single(x => x.Type == ClaimTypes.Name).Value; //pegando usuario logado
            //var produtosCache = await _cache.GetAsync("MeusProdutos");
            IList<ProdutoDTO>? produtos;
            //if (usuario.email == email)
            //{

            //    if (!string.IsNullOrWhiteSpace(produtosCache))
            //    {

            //        produtos = JsonConvert.DeserializeObject<IList<ProdutoDTO>>(produtosCache);
            //        return Ok(produtos);

            //    }
            //    else
            //    {
            //        var produtoClienteList = produtoClienteRepository.GetAllByUsuario(usuarioId).Result;

            //        IList<ProdutoDTO> prods = new List<ProdutoDTO>();

            //        foreach (var produtoCliente in produtoClienteList)
            //        {
            //            ProdutoDTO produto = new ProdutoDTO();
            //            produto.serial = produtoCliente.produto.serial;
            //            produto.nome = produtoCliente.produto.tipoProduto.nome;
            //            produto.descricao = produtoCliente.produto.tipoProduto.descricao;
            //            produto.fotoProduto = produtoCliente.produto.tipoProduto.fotoProduto;

            //            prods.Add(produto);
            //        }

            //        await _cache.SetAsync("MeusProdutos", JsonConvert.SerializeObject(prods));
            //        return Ok(prods);

            //    }


            //}
            //else
            //{
                var produtoClienteList = produtoClienteRepository.GetAllByUsuario(usuarioId).Result;

                IList<ProdutoDTO> prods = new List<ProdutoDTO>();

                foreach (var produtoCliente in produtoClienteList)
                {
                    ProdutoDTO produto = new ProdutoDTO();
                    produto.serial = produtoCliente.produto.serial;
                    produto.nome = produtoCliente.produto.tipoProduto.nome;
                    produto.descricao = produtoCliente.produto.tipoProduto.descricao;
                    //produto.fotoProduto = produtoCliente.produto.tipoProduto.fotoProduto;

                    //produto.imagens = produtoCliente.produto.tipoProduto.imagens; (Não está puxando as imagens)
                    produto.imagens = tipoProdutoImagemRepository.GetAllByTipoProduto(produtoCliente.produto.tipoProduto.id).Result.ToList();

                    prods.Add(produto);
                }
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
            //produtoDTO.fotoProduto = produto.tipoProduto.fotoProduto;
            produtoDTO.imagens = tipoProdutoImagemRepository.GetAllByTipoProduto(produto.tipoProduto.id).Result.ToList();

            return Ok(produtoDTO);
            //return new CreatedAtRouteResult("GetProdutoByÌd", new { id = produtoCliente.produtoid }, produtoCliente);
        }
    }
}
