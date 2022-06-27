using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNotaFiscal.Models
{
    public class Cliente
    {
        
        [Key]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Not Null")]
        [MaxLength(20)]
        public string TelefoneCliente{ get; set; } = null!;

        [Required(ErrorMessage = "Not Null")]
        [MaxLength(80)]
        public string Nome { get; set; } = null!;
        
        [Required(ErrorMessage = "Not Null")]
        [MaxLength(15)]
        public string CPF {get;set;} = null!;

        [System.Text.Json.Serialization.JsonIgnore]
        
        public Compra? Compra;
        
        [Required(ErrorMessage = "Not Null")]
        [MaxLength(60)]
        public string EmailCliente { get; set; } = null!;

        [ForeignKey("Endereco")]
        public int? IdEndereco { get; set; }
        public Endereco? Endereco;

    }
}