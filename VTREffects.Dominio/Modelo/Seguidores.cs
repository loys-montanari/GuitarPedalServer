using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtrEffects.Dominio.Modelo
{
    public class Seguidores
    {
        public int id { get; set; }
        public int seguidorid { get; set; }
        public  Usuario seguidor  { get; set; }

        public int usuarioid { get; set; }
        public Usuario usuario { get; set; }
    }
}
