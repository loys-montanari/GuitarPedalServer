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

        [Column("usuarioid")]
        public int usuarioid { get; set; }
        public virtual Usuario? usuario { get; set; }

        [Column("postagemid")]
        public int postagemid { get; set; }
        public virtual Postagem? postagem { get; set; }
    }
}
