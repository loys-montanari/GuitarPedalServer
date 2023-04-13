using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTREffects.Dominio.Models
{

    [Table("Contato")]
    public class Contato
    {
        [Key]
        private int Id { get; set; }
        private string TipoContato { get; set; }
        private string LinkContato { get; set; }

        public Contato(int id, string tipo, string link) {
        
        
                Id = id;
            TipoContato = tipo; 
            LinkContato = link;
        
        
        }
    }
}
