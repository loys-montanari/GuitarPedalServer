using System.ComponentModel.DataAnnotations.Schema;

namespace VtrEffects.Dominio.Modelo
{
    public class Comentario
    {
        public int id { get; set; }
        public string texto { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataExclusao { get; set; }
        [Column("IdUsuario")]
        public int idUsuario { get; set; }
        public virtual Usuario usuario { get; set; }

        [Column("IdPostagem")]
        public int idPostagem { get; set; }
        public virtual Postagem postagem { get; set; }


    }
}
