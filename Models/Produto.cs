using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNotaFiscal.Models
{
    public class Produto
    {
        [Key]
        public int idProduto {get;set;}

        public string NumChassi { get; set; } = null!;

        public string cor { get; set; } = null!;

        public string Modelo { get; set; } = null!;
        public string Fabricante { get; set; } = null!;
        public string VersaoDoSistema { get; set; } = null!;


        [ForeignKey("concessionaria")]
        public int? IdConcessionaria { get; set; }
        public Concessionaria? concessionaria;
        
        

        
        public Compra? compra;

        public DateTime Ano { get; set; }

        public double Valor { get; set; }
        public double Quilometragem { get; set; }

    }
}