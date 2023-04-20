namespace VtrEffects.Dominio.Modelo
{
    public class Postagem
    {
        public int Id { get; set; }
        public string Assunto { get; set; }
        public string Texo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataExclusao { get; set; }

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

    }
}
