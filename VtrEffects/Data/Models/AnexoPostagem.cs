using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VtrEffects.Models
{
    public class AnexoPostagem
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("IdPostagem")]
        public int IdPostagem { get; set; }
        public virtual Postagem Postagem { get; set; }

        [Column("NomeAnexo")]
        public string NomeAnexo { get; set; }

        [Column("Extensao")]
        public string Extensao { get; set; }
      
        [Column("Tamanho")]
        public int Tamanho { get; set; }

        [Column("Anexo")]
        public byte[] Anexo { get; set; }

    }
}
