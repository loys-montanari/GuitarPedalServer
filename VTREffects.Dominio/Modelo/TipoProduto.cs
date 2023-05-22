using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtrEffects.Dominio.Modelo
{
    public class TipoProduto
    {
        public int id { get; set; }
        public string nome { get; set; }

        public string descricao { get; set; }

        public string linkManual { get; set; }

        public string linkVideo { get; set; }

        public string versaoAtual { get; set; }

        //public string fotoProduto { get; set; }
        public List<TipoProdutoImagem> imagens { get; set; }
    }
}
