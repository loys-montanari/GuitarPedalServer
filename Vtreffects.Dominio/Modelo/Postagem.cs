namespace VtrEffects.Dominio.Modelo
{
    public class Postagem
    {
        public int id { get; set; }
        public string assunto { get; set; }
        public string texto { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataExclusao { get; set; }

        public int idUsuario { get; set; }
        public Usuario Usuario { get; set; }

    }
}
