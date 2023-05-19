using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VtrEffects.Helpers;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffects.DTO;
using VtrEffectsDados.Data.Repositorio;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using VtrEffects.Caching;
using Newtonsoft.Json;

namespace VtrEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository usuarioRep;
        private ISeguidoresRepository seguidoresRep;
        private readonly IConfiguration configuration;
        private ITipoProdutoRepository tipoProdutoRepository;
        private ICachingService _cache;

        public LoginController(ICachingService cache, ITipoProdutoRepository _tipoProdutoRepository, IUsuarioRepository userrepository, ISeguidoresRepository seguidoresrepository, IConfiguration _configuration)
        {
            usuarioRep = userrepository;
            seguidoresRep = seguidoresrepository;
            configuration = _configuration;
            this.tipoProdutoRepository = _tipoProdutoRepository;
            this._cache = cache;
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

                retorno.token = GeraToken(logininfo);

                return Ok(retorno);
            }
        }

        private TokenDTO GeraToken(LoginDTO login)
        {
            //define declarações do usuário
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, login.email),                
                new Claim("random", "words"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                
            };

            //gera uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:key"]));

            //Gera assinatura digital do token usando o algoritmo hmac e a chave privada
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiração do token
            var expiration = configuration["TokenConfiguration:ExpireHours"];
            var expiracao = DateTime.UtcNow.AddHours(double.Parse(expiration));

            //Classe que representa um token JWT e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["TokenConfiguration:Issuer"],
                audience: configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiracao,
                signingCredentials: credentials);

            //Retorna os dados com o token e informações
            return new TokenDTO()
            {
                Autenticado = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracao = expiracao,
                Mensagem = "Token JWT"
            };
        }


        [HttpGet("produtos")]
        public async Task<ActionResult<IList<TipoProduto>>> GetAllProdutos()
        {

            var produtosCache = await _cache.GetAsync("Produtos");

            IList<TipoProduto>? produtos;
            if (!string.IsNullOrWhiteSpace(produtosCache))
            {

                produtos = JsonConvert.DeserializeObject<IList<TipoProduto>>(produtosCache);
              
            }
            else
            {
                 produtos = await tipoProdutoRepository.GetAllAsync();
                if (produtos == null)
                    return BadRequest("Nenhum produto encontrado");
              

            }

            await _cache.SetAsync("Produtos", JsonConvert.SerializeObject(produtos));
            return Ok(produtos);

        }


    }
}
