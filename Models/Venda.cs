using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiNotaFiscal.Models
{

    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }

        public Compra? compra;

        public float ValorTotal { get; set; }
        public string? Comentario { get; set; }

        [Required(ErrorMessage = "Not Null")]
        [MaxLength(60)]
        public string tipoPagamento { get; set; } = null!;

        public NotaFiscal? notaFiscal;
    }
}