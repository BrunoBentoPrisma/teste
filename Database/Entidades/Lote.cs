using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class Lote : BaseEntity
    {
        [Column("FornecedorId")]
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; } = null!;
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("GrupoId")]
        public Guid? GrupoId { get; set; }
        [Column("DataDeEntradaDoLote")]
        public DateTime DataDeEntradaDoLote { get; set; }
        [Column("DataDeFabricacaoDoLote")]
        public DateTime DataDeFabricacaoDoLote { get; set; }
        [Column("DataDeValidadeDoLote")]
        public DateTime DataDeValidadeDoLote { get; set; }
        [Column("NumeroNota")]
        [StringLength(20)]
        public string NumeroNota { get; set; } = string.Empty;
        [Column("SerieNota")]
        [StringLength(10)]
        public string SerieNota { get; set; } = string.Empty;
        [Column("QuantidadeComprometidaLote", TypeName = "decimal(24,4)")]
        public decimal QuantidadeComprometidaLote { get; set; }
    }
}