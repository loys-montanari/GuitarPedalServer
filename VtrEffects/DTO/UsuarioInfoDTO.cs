using VtrEffects.Dominio.Modelo;

namespace VtrEffects.DTO
{
    public class UsuarioInfoDTO
    {
        public Usuario usuario { get; set; }

        public int seguidores { get; set; }

        public int seguindo { get; set; }

        public TokenDTO? token { get; set; }
    }

    
}
