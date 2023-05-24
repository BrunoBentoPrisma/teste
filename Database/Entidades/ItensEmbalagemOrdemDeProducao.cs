using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class ItensEmbalagemOrdemDeProducao : BaseEntity
    {
        [Column("EmbalagemOrdemDeProducaoId")]
        public Guid EmbalagemOrdemDeProducaoId { get; set; }
        [Column("Sequencia")]
        public int Sequencia { get; set; }
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("FatorDeCorrecao", TypeName = "decimal(14,4)")]
        public decimal FatorDeCorrecao { get; set; }
        [Column("LoteInterno")]
        public int LoteInterno { get; set; }
        [Column("QuantidadeComprometidaLote", TypeName = "decimal(24,4)")]
        public decimal QuantidadeComprometidaLote { get; set; }
        [Column("QuantidadePesada", TypeName = "decimal(24,4)")]
        public decimal QuantidadePesada { get; set; }
        [Column("QuantidadeSemFator", TypeName = "decimal(24,4)")]
        public decimal QuantidadeSemFator { get; set; }
        [Column("QuantidadeUnidadeEstoque", TypeName = "decimal(24,4)")]
        public decimal QuantidadeUnidadeEstoque { get; set; }
        [Column("OrdemDeProducaoId")]
        public Guid? OrdemDeProducaoId { get; set; }
        [Column("GrupoId")]
        public Guid? GrupoId { get; set; }
    }
}