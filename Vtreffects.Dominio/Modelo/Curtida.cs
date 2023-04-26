using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtrEffects.Dominio.Modelo
{
    public class Curtida
    {
        [Key]
        [Column("Id")]
        public int id { get; set; }
        [Column("Tipo")]
        public int tipo { get; set; }

        [Column("IdUsuario")]
        public int idUsuario { get; set; }
        public virtual Usuario usuario { get; set; }

        [Column("IdPostagem")]
        public int idPostagem { get; set; }
        public virtual Postagem postagem { get; set; }
    }
}
