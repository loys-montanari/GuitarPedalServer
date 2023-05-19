using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json.Serialization;
using VtrEffects.Caching;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffects.DTO;
using VtrEffects.Helpers;

namespace VtrEffects.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private IUsuarioRepository usuarioRep;
        private IPostagemRepository postagemRep;
        private ICurtidaRepository curtidaRep;
        private IComentarioRepository comentarioRep;
        private IAnexoRepository anexoRep;
        private ICachingService _cache;

        public ForumController(ICachingService cache, IUsuarioRepository usuarioRep, IPostagemRepository postagemRep, ICurtidaRepository curtidaRep, IComentarioRepository comentarioRep, IAnexoRepository anexoRep)
        {
            this.usuarioRep = usuarioRep;
            this.postagemRep = postagemRep;
            this.curtidaRep = curtidaRep;
            this.comentarioRep = comentarioRep;
            this.anexoRep = anexoRep;
            this._cache = cache;
        }


        [HttpPost]
        public async Task<ActionResult<Postagem>> AddPostagem(ForumDTO postagem)
        {
            ForumDTO retorno = new ForumDTO();
            if (postagem == null)
                return BadRequest("Mande um post válido");

            var post = await postagemRep.SaveAsync(postagem.post);
            if (post == null)
                return BadRequest("Houve um problema ao salvar seu post.");


            retorno.post = post;
            if (postagem.anexosPostagem != null)
            {
                retorno.anexosPostagem = new List<AnexoPostagem>();

                foreach (AnexoPostagem a in postagem.anexosPostagem)
                {


                    retorno.anexosPostagem.Add(await anexoRep.SaveAsync(a));


                }
            }

            return Ok(retorno);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Postagem>> DeletePostagem(int id)
        {
            var post = postagemRep.GetById(id);
            if (post == null) return BadRequest("Postagem não existente");


            post.dataExclusao = DateTime.Now;
            await postagemRep.UpdateAsync(post);

            return Ok();

        }

        [HttpGet]
        public async Task<ActionResult<List<ForumDTO>>> getAllPostagens()
        {

            var list = new List<ForumDTO>();
            var posts = await postagemRep.getAllNotDeleted();
            var email = User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
 
            var user = await usuarioRep.GetByEmail(email);

            foreach (Postagem p in posts) {
                var curtidauser = await curtidaRep.getByUsuarioAndPost(user.id, p.id);
                ForumDTO dto = new ForumDTO();
                dto.post = p;
                dto.comentarios = await comentarioRep.getAllByPost(p.id);
                dto.curtidas = new List<Curtida>();
                dto.naocurtidas = new List<Curtida>();
                
                if(curtidauser  != null) {
                    dto.curtidobyuser = true;
                    dto.tipocurtidauser = curtidauser.tipo;
                }
                else
                {
                    dto.curtidobyuser = false;
                }

                foreach (Curtida c in await curtidaRep.getAllByPost(p.id))
                {
                    if(c.tipo ==1)
                        dto.curtidas.Add(c);
                    if (c.tipo == 2)
                        dto.naocurtidas.Add(c);

                }


                dto.anexosPostagem = await anexoRep.getAllByPost(p.id);


                list.Add(dto);

            }


            return Ok(list.OrderByDescending(x => x.post.dataCriacao));
        }

        [HttpGet("{idpost}")]
        public async Task<ActionResult<ForumDTO>> getPost(int idpost)
        {           
            var postCache = await _cache.GetAsync(idpost.ToString());


            Postagem? post;
            ForumDTO? dto = new ForumDTO();
            if (!string.IsNullOrWhiteSpace(postCache))
            {

                dto = JsonConvert.DeserializeObject<ForumDTO>(postCache);
                if (dto.post.dataExclusao != null) return BadRequest("Postagem excluida");
            }
            else
            {
                post = postagemRep.GetById(idpost);
                if (post == null) return BadRequest("Postagem não encontrada");

                if (post.dataExclusao != null) return BadRequest("Postagem excluida");
                dto.post = post;
                dto.comentarios = await comentarioRep.getAllByPost(post.id);
                dto.curtidas = await curtidaRep.getAllByPost(post.id);
                dto.anexosPostagem = await anexoRep.getAllByPost(post.id);

            }

            await _cache.SetAsync(idpost.ToString(), JsonConvert.SerializeObject(dto));

            return Ok(dto);
        }

        [HttpPost(), Route("AddComentario")]
        public async Task<ActionResult<Postagem>> AddComentario(Comentario coment)
        {

            if (coment == null) return BadRequest("Envie um comentário válido");

            await comentarioRep.SaveAsync(coment);

            return Ok(coment);

        }

        [HttpDelete("DeleteComentario/{id}"), Route("DeleteComentario")]
        public async Task<ActionResult<Comentario>> DeleteComentario(int id)
        {
            var coment = comentarioRep.GetById(id);
            if (coment == null) return BadRequest("Envie um id válido");


            coment.dataExclusao = DateTime.Now;
            await comentarioRep.UpdateAsync(coment);

            return Ok();

        }

        [HttpPost(), Route("Curtida")]
        public async Task<ActionResult<bool>> curtida(Curtida curtida)
        {
            var curt = await curtidaRep.getByUsuarioAndPost(curtida.usuarioid,curtida.postagemid);
            if (curt == null)
            {
                await curtidaRep.SaveAsync(curtida);
                return Ok(true);

                //se não existe curtida nesse post para esse usuario, ele vai criar
            }
            else
            {
                //caso contrário, se for uma curtida do outro tipo, ele deleta a primeira e cria a nova, se não ele só deleta a primeira

                if (curt.tipo != curtida.tipo)
                {
                    await curtidaRep.SaveAsync(curtida);
                    
                }
                await curtidaRep.DeleteAsync(curt);
                return Ok(false);
            }

        }

        [HttpGet("user/{id}"), Route("user")]
        public async Task<ActionResult<List<ForumDTO>>> getAllPostagensByUsuer(int id)
        {

            var list = new List<ForumDTO>();
            var posts = await postagemRep.getAllNotDeletedByUser(id);

            if (posts == null)
                return Ok("Este usuário não tem nenhum post");

            foreach (Postagem p in posts)
            {

                ForumDTO dto = new ForumDTO();
                dto.post = p;
                dto.comentarios = await comentarioRep.getAllByPost(p.id);
                dto.curtidas = await curtidaRep.getAllByPost(p.id);
                dto.anexosPostagem = await anexoRep.getAllByPost(p.id);


                list.Add(dto);

            }


            return Ok(list);
        }


        [HttpGet("count"), Route("count")]
        public async Task<ActionResult<int>> getQuantosPosts()
        {

            int qtd = await postagemRep.QtdPosts();


            return Ok(qtd);
        }
    }
}
