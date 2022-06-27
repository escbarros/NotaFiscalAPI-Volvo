using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNotaFiscal.Models
{
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("Veiculos")]
        public int? IdProduto { get; set; }

        public Produto? produto;

        [ForeignKey("Clientes")]
        public int? IdCliente{ get; set; }

        public Cliente? cliente;

        [ForeignKey("Venda")]
        public int? IdVenda { get; set; }

        public Venda? venda;
        
        [ForeignKey("funcionario")]
        public int? IdFuncionario {get;set;}
        public Funcionario?funcionario;
    }
}