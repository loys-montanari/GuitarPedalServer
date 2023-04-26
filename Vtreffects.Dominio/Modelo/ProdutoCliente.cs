using System.ComponentModel.DataAnnotations.Schema;
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Modelo
{
    [Table("ProdutoCliente")]
    public class ProdutoCliente
    {
        public int id { get; set; }


        public int idProduto { get; set; }
        public Produto produto { get; set; }

        public int idCliente { get; set; }
        public Usuario usuario { get; set; }

        public string serial { get; set; }
        public bool ativo { get; set; }
        public bool primeiroComprador { get; set; }
    }
}
