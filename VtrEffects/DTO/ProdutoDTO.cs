﻿using VtrEffects.Dominio.Modelo;

namespace VtrEffects.DTO
{
    public class ProdutoDTO
    {
        public string serial { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string fotoProduto { get; set; }

        public string fotoPng { get; set; }
        public string? dataCompra { get; set; }
        public string? dataGarantia { get; set; }
        public string? linkManual { get; set; }
    }
}
