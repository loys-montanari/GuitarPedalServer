using System.ComponentModel.DataAnnotations.Schema;
using VtrEffects.Models;

namespace VtrEffects.Data.Models
{
    [Table("ProdutoCliente")]
    public class ProdutoCliente
    {
        public int Id { get; set; }


        public int IdProduto { get; set; }
        public Produto produto { get; set; }

        public int IdCliente { get; set; }
        public Usuario usuario { get; set; }

        public string Serial { get; set; }
        public bool Ativo { get; set; }
        public bool PrimeiroComprador { get; set; }
    }
}
