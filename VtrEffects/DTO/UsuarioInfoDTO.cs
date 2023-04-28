using VtrEffects.Dominio.Modelo;

namespace VtrEffects.DTO
{
    public class UsuarioInfoDTO
    {
        public Usuario usuario { get; set; }

        public List<Seguidores> seguidores { get; set; }

        public List<Seguidores> seguindo { get; set; }


    }


}
