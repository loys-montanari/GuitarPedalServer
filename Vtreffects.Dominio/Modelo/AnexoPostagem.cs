using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtrEffects.Dominio.Modelo
{
    public class AnexoPostagem
    {
        [Key]
        [Column("Id")]
        public int id { get; set; }
        [Column("IdPostagem")]
        public int idPostagem { get; set; }
        public virtual Postagem postagem { get; set; }

        [Column("NomeAnexo")]
        public string nomeAnexo { get; set; }

        [Column("Extensao")]
        public string extensao { get; set; }

        [Column("Tamanho")]
        public int tamanho { get; set; }

        [Column("Anexo")]
        public byte[] anexo { get; set; }

    }
}
