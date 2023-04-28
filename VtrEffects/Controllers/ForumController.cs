using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffects.DTO;
using VtrEffects.Helpers;

namespace VtrEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private IUsuarioRepository usuarioRep;
        private IPostagemRepository postagemRep;
        private ICurtidaRepository curtidaRep;
        private IComentarioRepository comentarioRep;
        private IAnexoRepository anexoRep;


        public ForumController(IUsuarioRepository usuarioRep, IPostagemRepository postagemRep, ICurtidaRepository curtidaRep, IComentarioRepository comentarioRep, IAnexoRepository anexoRep)
        {
            this.usuarioRep = usuarioRep;
            this.postagemRep = postagemRep;
            this.curtidaRep = curtidaRep;
            this.comentarioRep = comentarioRep;
            this.anexoRep = anexoRep;   
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

                foreach(AnexoPostagem a in postagem.anexosPostagem)
                {
                   

                    retorno.anexosPostagem.Add(await anexoRep.SaveAsync(a));


                }
            }
            
            return Ok(retorno);

        }

        [HttpDelete()]
        public async Task<ActionResult<Postagem>> DeletePostagem(int id)
        {
            var post =  postagemRep.GetById(id);
            if (post == null) return BadRequest("Envie um id válido");


            post.dataExclusao = DateTime.Now;
            await postagemRep.UpdateAsync(post);

            return Ok();

        }


        [HttpPost(), Route("AddComentario")]
        public async Task<ActionResult<Postagem>> AddComentario(Comentario coment)
        {

            if (coment == null) return BadRequest("Envie um comentário válido");

            await comentarioRep.SaveAsync(coment);

            return Ok(coment);

        }


    }
}
