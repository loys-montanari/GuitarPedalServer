using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtrEffects.Data.Models
{

    [Table("Contato")]
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        public string TipoContato { get; set; }
        public string LinkContato { get; set; }

    }
}
