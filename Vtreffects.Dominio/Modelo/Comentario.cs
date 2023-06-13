using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VtrEffects.Dominio.Modelo
{
    public class Comentario
    {
        [Key]
        public int id { get; set; }
        public string texto { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime? dataExclusao { get; set; }
       
        [Column("usuarioid")]
        public int usuarioid { get; set; }
        //[JsonIgnore]
        public virtual Usuario? usuario { get; set; }

        [Column("postagemid")]
        public int postagemid { get; set; }
        
        [JsonIgnore]
        public   Postagem? postagem { get; set; }


        JsonSerializerOptions options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

    }
}
