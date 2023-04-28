using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtrEffects.Dominio.Modelo
{
    public class Produto
    {
        public int id { get; set; }

        
        public int idTipoProduto { get; set; }
        public virtual TipoProduto tipoProduto { get; set; }

        public string serial { get; set; }
    }
}
