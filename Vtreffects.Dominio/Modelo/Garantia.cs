using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtrEffects.Dominio.Modelo
{
    [Table("Garantia")]
    public class Garantia
    {
        [Key]
        public int id { get; set; }
        public string descricao { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataFim { get; set; }
        public int produtoClienteId { get; set; }
    }
}
