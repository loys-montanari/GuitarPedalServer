using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using VtrEffects.Caching;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffects.DTO;
using VtrEffectsDados.Data.Repositorio;

namespace VtrEffects.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : Controller
    {
        private IUsuarioRepository usuarioRep;
        private ISeguidoresRepository seguidoresRep;
        private IPostagemRepository postagemRep;
        private ICurtidaRepository curtidaRep;
        private IComentarioRepository comentarioRep;
        private IAnexoRepository anexoRep;
        private ICachingService _cache;
        private IProdutoClienteRepository produtoClienteRep;
        private ITipoProdutoImagemRepository tipProdutoImagemRep;

        public PerfilController(ITipoProdutoImagemRepository tipProdutoImagemRep, IUsuarioRepository usuarioRepository, ISeguidoresRepository seguidoresRepository, IPostagemRepository postagemRepository, ICurtidaRepository curtidaRep, IComentarioRepository comentarioRep, IAnexoRepository anexoRep, ICachingService caching, IProdutoClienteRepository produtoCliente)
        {
            usuarioRep = usuarioRepository;
            seguidoresRep = seguidoresRepository;
            postagemRep = postagemRepository;
            this.curtidaRep = curtidaRep;
            this.comentarioRep = comentarioRep;
            this.anexoRep = anexoRep;
            this._cache = caching;
            this.produtoClienteRep = produtoCliente;
            this.tipProdutoImagemRep = tipProdutoImagemRep;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<PerfilDTO>> GetByUsuario(int usuarioId)
        {
            var email = User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;

            var userlogado = await usuarioRep.GetIdByEmail(email);
            var dadousercache = await _cache.GetAsync(usuarioId.ToString());
            var followersusercache = await _cache.GetAsync($"seguidoresUsuario-{usuarioId}");
            var followingusercache = await _cache.GetAsync($"seguindosUsuario-{usuarioId}");
            var postsusercache = await _cache.GetAsync($"PostsUsuario-{usuarioId}");
            var produtosusercache = await _cache.GetAsync($"ProdutosUsuario-{usuarioId}");

            var usuario = string.IsNullOrWhiteSpace(dadousercache) ? usuarioRep.GetById(usuarioId) : JsonConvert.DeserializeObject<Usuario>(dadousercache); 
            if (usuario == null)
                return BadRequest();

            PerfilDTO perfil = new PerfilDTO();
            perfil.seguidoPorUsuarioLogado = await seguidoresRep.isSeguidoPorUsuario(userlogado);
            perfil.usuario = usuario;
            perfil.followers = string.IsNullOrWhiteSpace(followersusercache) ? seguidoresRep.GetAllByUsuarioAsync(usuario.id).Result : JsonConvert.DeserializeObject<List<Seguidores>>(followersusercache); 
            perfil.following = string.IsNullOrWhiteSpace(followingusercache) ?  seguidoresRep.GetAllSeguidosAsync(usuario.id).Result : JsonConvert.DeserializeObject<List<Seguidores>>(followingusercache);
           
           
            if(string.IsNullOrWhiteSpace(produtosusercache))
            {
                var produtos = produtoClienteRep.GetAllByUsuario(usuarioId).Result;
                if (produtos != null)
                {
                    foreach (var produtoCliente in produtos)
                    {
                        ProdutoDTO produto = new ProdutoDTO();
                        produto.serial = produtoCliente.produto.serial;
                        produto.nome = produtoCliente.produto.tipoProduto.nome;
                        produto.descricao = produtoCliente.produto.tipoProduto.descricao;
                        //produto.fotoProduto = produtoCliente.produto.tipoProduto.fotoProduto;

                        //produto.imagens = produtoCliente.produto.tipoProduto.imagens; (Não está puxando as imagens)
                        produto.fotoProduto = tipProdutoImagemRep.GetByTipoProdutoAndTipoImagem(produtoCliente.produto.tipoProduto.id, 2).Result.imagem;
                        produto.fotoPng = tipProdutoImagemRep.GetByTipoProdutoAndTipoImagem(produtoCliente.produto.tipoProduto.id, 1).Result.imagem;

                        perfil.produtos.Add(produto);
                    }
                    

                }
                
            }
            else
            { 
                perfil.produtos = JsonConvert.DeserializeObject<List<ProdutoDTO>>(produtosusercache);

            }
            
            
            
            var listposts = string.IsNullOrWhiteSpace(postsusercache)  ? await postagemRep.getAllNotDeletedByUser(usuario.id) : JsonConvert.DeserializeObject<List<Postagem>>(postsusercache);
            await _cache.SetAsync($"PostsUsuario-{usuarioId}", JsonConvert.SerializeObject(listposts));
            if (listposts != null)
            {

                foreach (Postagem p in listposts)
                {
                    var comentarioscache = await _cache.GetAsync($"comentariosPost-{p.id}");
                    var curtidascache = await _cache.GetAsync($"curtidasPost-{p.id}");
                    var naocurtidascache = await _cache.GetAsync($"naoCurtidasPost-{p.id}");
                    var anexoscache = await _cache.GetAsync($"anexosPost-{p.id}");

                    var curtidauser = await curtidaRep.getByUsuarioAndPost(userlogado, p.id);
                    ForumDTO dto = new ForumDTO();
                    dto.post = p;
                    dto.comentarios = string.IsNullOrWhiteSpace(comentarioscache) ? await comentarioRep.getAllByPost(p.id) : JsonConvert.DeserializeObject<List<Comentario>>(comentarioscache); 
                    dto.curtidas =  string.IsNullOrWhiteSpace(curtidascache) ? await curtidaRep.getAllByPostTipo(p.id, 1) : JsonConvert.DeserializeObject<List<Curtida>>(curtidascache);
                    dto.naocurtidas = string.IsNullOrWhiteSpace(naocurtidascache) ? await curtidaRep.getAllByPostTipo(p.id, 2) : JsonConvert.DeserializeObject<List<Curtida>>(naocurtidascache);
                    dto.anexosPostagem = string.IsNullOrWhiteSpace(anexoscache) ? await anexoRep.getAllByPost(p.id) : JsonConvert.DeserializeObject<List<AnexoPostagem>>(anexoscache);

                    if (curtidauser != null)
                    {
                        dto.curtidobyuser = true;
                        dto.tipocurtidauser = curtidauser.tipo;
                    }
                    else
                    {
                        dto.curtidobyuser = false;
                    }

                     perfil.posts.Add(dto);

                     await _cache.SetAsync($"comentariosPost-{p.id}", JsonConvert.SerializeObject(dto.comentarios));
                     await _cache.SetAsync($"curtidasPost-{p.id}", JsonConvert.SerializeObject(dto.curtidas));
                     await _cache.SetAsync($"naoCurtidasPost-{p.id}", JsonConvert.SerializeObject(dto.naocurtidas));
                     await _cache.SetAsync($"anexosPost-{p.id}", JsonConvert.SerializeObject(dto.anexosPostagem));


                }

            }
             await _cache.SetAsync(usuarioId.ToString(), JsonConvert.SerializeObject(perfil.usuario));
             await _cache.SetAsync($"seguidoresUsuario-{usuarioId}", JsonConvert.SerializeObject(perfil.followers));
             await _cache.SetAsync($"seguindosUsuario-{usuarioId}", JsonConvert.SerializeObject(perfil.following));
             
             await _cache.SetAsync($"ProdutosUsuario-{usuarioId}", JsonConvert.SerializeObject(perfil.produtos));
            return Ok(perfil);
        }


        [HttpPost("seguir/{usuarioId}")]
        public async Task<ActionResult<bool>> seguir(int usuarioId)
        {
            var email = User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;

            var userlogado = await usuarioRep.GetIdByEmail(email);
            var dadousercache = await _cache.GetAsync(usuarioId.ToString());
            var followersusercache = await _cache.GetAsync($"seguidoresUsuario-{usuarioId}");



                List<Seguidores> seguidores = string.IsNullOrWhiteSpace(followersusercache) ? seguidoresRep.GetAllByUsuarioAsync(usuarioId).Result : JsonConvert.DeserializeObject<List<Seguidores>>(followersusercache);
                if(seguidores != null)
                 {
                
                    bool seguindo = seguidores.Any(x => x.seguidorid == userlogado);
                   if (seguindo)
                 {
                    Seguidores seguidor = seguidores.FirstOrDefault(x => x.seguidorid == userlogado);
                    seguidoresRep.DeleteAsync(seguidor);
                    seguidores.RemoveAll(x => x.seguidorid.Equals(userlogado));
                    await _cache.SetAsync($"seguidoresUsuario-{usuarioId}", JsonConvert.SerializeObject(seguidores));
                    return Ok(false);

                }
                 else
                {
                    Seguidores seguidor = new Seguidores { seguidorid = userlogado, usuarioid = usuarioId };
                    seguidor = await seguidoresRep.SaveAsync(seguidor);
                    seguidores.Add(seguidor);
                    await _cache.SetAsync($"seguidoresUsuario-{usuarioId}", JsonConvert.SerializeObject(seguidores));

                    return Ok(true);
                }

     
                }
                else
                 {
                List<Seguidores> seguidoresnovos = new List<Seguidores>();
                Seguidores seguidor = new Seguidores { seguidorid = userlogado, usuarioid = usuarioId };
                seguidor = await seguidoresRep.SaveAsync(seguidor);
                seguidoresnovos.Add(seguidor);
                await _cache.SetAsync($"seguidoresUsuario-{usuarioId}", JsonConvert.SerializeObject(seguidoresnovos));
                return Ok(true);

            }



        }
}
}
