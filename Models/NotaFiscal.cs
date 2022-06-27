using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNotaFiscal.Models
{
    public class NotaFiscal
    {
        [Key]
        public int IdNotaFiscal { get; set; }

        public int TributaçãoImposto { get; set; }


        [ForeignKey("id")]
        public int? IdVenda { get; set; }
        public Venda? venda;


        [ForeignKey("id")]
        public int? IdConcessionaria { get; set; }

        public Concessionaria? concessionaria;
    }
}