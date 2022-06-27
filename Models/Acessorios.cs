
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNotaFiscal.Models
{
    public partial class Acessorio
    {
        [Key]
        public int IdAcessorio { get; set; }

        [ForeignKey("produto")]
        public int? IdProduto { get; set; }
        public Produto? produto;

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Funcionario? IdFuncionarioNavigation { get; set; }

        

    }
}