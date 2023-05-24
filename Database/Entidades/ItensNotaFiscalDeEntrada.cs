using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class ItensNotaFiscalDeEntrada : BaseEntity
    {
        [Column("ProdutoId")]
        [Required]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("NotaFiscalDeEntradaId")]
        [Required]
        public Guid NotaFiscalDeEntradaId { get; set; }
        [Column("ValorUnitario", TypeName = "decimal(24,4)")]
        public decimal ValorUnitario { get; set; }
        [Column("Total", TypeName = "decimal(24,4)")]
        public decimal Total { get; set; }
        [Column("Frete", TypeName = "decimal(24,4)")]
        public decimal Frete { get; set; }
        [Column("Quantidade", TypeName = "decimal(24,4)")]
        public decimal Quantidade { get; set; }
        [Column("ValorIpi", TypeName = "decimal(24,4)")]
        public decimal ValorIpi { get; set; }
        [Column("Validade")]
        public DateTime Validade { get; set; }
        public string? LoteFornecedorEntrada { get; set; }
        public NotaFiscalDeEntrada NotaFiscalDeEntrada { get; set; } = null!;
    }
}