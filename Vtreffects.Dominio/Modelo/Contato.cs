using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtrEffects.Dominio.Modelo
{

    [Table("Contato")]
    public class Contato
    {
        [Key]
        public int id { get; set; }
        public string tipoContato { get; set; }
        public string linkContato { get; set; }

    }
}
