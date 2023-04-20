using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtrEffects.Dominio.Modelo
{
    public class Curtida
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Tipo")]
        public int Tipo { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Column("IdPostagem")]
        public int IdPostagem { get; set; }
        public virtual Postagem Postagem { get; set; }
    }
}
