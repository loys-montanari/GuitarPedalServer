using System.ComponentModel.DataAnnotations.Schema;

namespace VtrEffects.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataExclusao { get; set; }
        [Column("IdPostagem")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Column("IdPostagem")]
        public int IdPostagem { get; set; }
        public virtual Postagem Postagem { get; set; }


    }
}
