using VtrEffects.Dominio.Modelo;

namespace VtrEffects.DTO
{
    public class ForumDTO
    {
        public Postagem post { get; set; }

        public  List<AnexoPostagem>? anexosPostagem { get; set; }

        public  List<Curtida>? curtidas { get; set; }

        public  List<Comentario>? comentarios { get; set; }

    }
}
