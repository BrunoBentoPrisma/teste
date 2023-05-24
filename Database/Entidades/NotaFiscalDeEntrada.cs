using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class NotaFiscalDeEntrada : BaseEntity
    {
        [Column("FornecedorId")]
        [Required]
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; } = null!;
        [Column("DataDeEmissao")]
        public DateTime DataDeEmissao { get; set; }
        [Column("DataDeEntrada")]
        public DateTime DataDeEntrada { get; set; }
        [Column("NumeroNota")]
        [Required]
        [StringLength(10)]
        public string NumeroNota { get; set; } = string.Empty;
        [Column("SerieNota")]
        [Required]
        [StringLength(5)]
        public string SerieNota { get; set; } = string.Empty;
        [Column("Total", TypeName = "decimal(24,4)")]
        [Required]
        public decimal Total { get; set; }
        [Column("Frete", TypeName = "decimal(24,4)")]
        [Required]
        public decimal Frete { get; set; }
        public List<ItensNotaFiscalDeEntrada> ItensNotaFiscalDeEntrada { get; set; } = null!;

    }
}