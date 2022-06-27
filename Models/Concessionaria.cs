using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiNotaFiscal.Controllers;

namespace WebApiNotaFiscal.Models
{
    public class Concessionaria
    {
        [Key]
        public int IdConcessionaria { get; set; }
        public string Cnpj { get; set; } = null!;
        
        [Required(ErrorMessage = "Not Null")]
        [MaxLength(45)]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Not Null")]
        [MaxLength(20)]
        public string Telefone { get; set; } = null!;
        public DateTime? DataFundacao { get; set; }
        public string? RazaoSocial { get; set; }


        [System.Text.Json.Serialization.JsonIgnore]         
        public Produto? produto;

        [System.Text.Json.Serialization.JsonIgnore]         
        public NotaFiscal? NotaFiscal;

        public EnderecoConcessionaria? enderecoConcessionaria;
    }
}