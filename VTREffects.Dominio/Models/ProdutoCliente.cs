using System.ComponentModel.DataAnnotations.Schema;

namespace VTREffects.Dominio.Models
{
    [Table("ProdutoCliente")]
    public class ProdutoCliente
    {
        public int Id { get; set; }

        
        public int IdProduto { get; set; }

        public Produto produto { get; set; }
    }
}
