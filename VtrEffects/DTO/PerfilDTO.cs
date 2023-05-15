using VtrEffects.Dominio.Modelo;

namespace VtrEffects.DTO
{
    public class PerfilDTO
    {
        public Usuario usuario { get; set; }
        public List<Seguidores> followers { get; set; } = new List<Seguidores>();
        public List<Seguidores> following { get; set; } = new List<Seguidores>();
        public int? qtdPosts { get; set; }
    }
}
