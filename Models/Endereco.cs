using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNotaFiscal.Models
{
    public class Endereco
    {
        [Key]
        public int IdEndereco { get; set; }

        [Required(ErrorMessage = "Not Null")]
        public string Cep { get; set; } = null!;
        public string? Complemento { get; set; }
        public int NumeroRua { get; set; }

        [Required(ErrorMessage = "Not Null")]
        public string rua { get; set; } = null!;

        [Required(ErrorMessage = "Bairo Not Null")]
        [MaxLength(45)]
        public string bairro { get; set; } = null!;

        [Required(ErrorMessage = "Not Null")]
        [MaxLength(45)]
        public string municipio {get;set;} = null!;

        [Required(ErrorMessage = "Not Null")]
        [MaxLength(2)]
        public string uf {get;set;} = null!;

        public Cliente? Cliente;

        
    }
}