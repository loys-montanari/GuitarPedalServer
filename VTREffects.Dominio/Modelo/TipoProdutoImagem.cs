using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VtrEffects.Dominio.Modelo
{
    public class TipoProdutoImagem
    {
        [JsonIgnore]
        public int id { get; set; }
        public string imagem { get; set; }
        public int ordem { get; set; }

        [JsonIgnore]
        public int tipoProdutoId { get; set; }

        [JsonIgnore]
        public TipoProduto? tipoProduto { get; set; }
    }
}
