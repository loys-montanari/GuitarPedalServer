﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTREffects.Dominio.Models
{
    [Table("Garantia")]
    public class Garantia
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataFim { get; set; }
        public int ProdutoClienteID { get; set; }
    }
}
