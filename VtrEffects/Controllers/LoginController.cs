﻿using Microsoft.AspNetCore.Http;
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
        private ITipoProdutoImagemRepository tipoProdutoImagemRepository;
        private ICachingService _cache;

        public LoginController(ITipoProdutoImagemRepository tipoProdutoImagemRepository, ICachingService cache, ITipoProdutoRepository _tipoProdutoRepository, IUsuarioRepository userrepository, ISeguidoresRepository seguidoresrepository, IConfiguration _configuration)
        {
            usuarioRep = userrepository;
            seguidoresRep = seguidoresrepository;
            configuration = _configuration;
            this.tipoProdutoRepository = _tipoProdutoRepository;
            this._cache = cache;
            this.tipoProdutoImagemRepository = tipoProdutoImagemRepository;
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
               
                retorno.seguidores = seguidoresRep.GetAllByUsuarioAsync(retorno.usuario.id).Result.Count;

                retorno.seguindo = seguidoresRep.GetAllSeguidosAsync(retorno.usuario.id).Result.Count;

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


    }
}
