using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNotaFiscal
{
    public class Funcionario
    {
        
        [Key]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "Not Null")]
        [MaxLength(80)]
        public string Nome { get; set; } = null!;
        
        [Required(ErrorMessage = "Not Null")]
        [MaxLength(15)]
        public string CPF {get;set;} = null!;

        [Required(ErrorMessage = "Not Null")]
        public int Salario { get; set; } 


    }
}