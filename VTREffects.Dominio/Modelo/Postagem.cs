using System.Text.Json.Serialization;

namespace VtrEffects.Dominio.Modelo
{
    public class Postagem
    {
        public int id { get; set; }
        public string assunto { get; set; }
        public string texto { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime? dataExclusao { get; set; }

        public int usuarioid { get; set; }

        [JsonIgnore]
        public virtual Usuario usuario { get; set; }


    }
}
