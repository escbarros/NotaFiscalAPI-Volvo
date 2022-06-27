using Microsoft.EntityFrameworkCore;

namespace WebApiNotaFiscal.Models
{
    public class SistemaNotaFiscal:DbContext
    {
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Concessionaria> Concessionaria { get; set; } = null!;
        public virtual DbSet<Endereco> Enderecos { get; set; } = null!;
        public virtual DbSet<EnderecoConcessionaria> EnderecoConcessionarias { get; set; } = null!;
        public virtual DbSet<NotaFiscal> NotaFiscals { get; set; } = null!;
        public virtual DbSet<Produto> Produtos { get; set; } = null!;
        public virtual DbSet<Venda> Venda { get; set; } = null!;
        public virtual DbSet<Funcionario> Funcionarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=SistemaNotaFiscal;Trusted_Connection=True");
        }
    }
}