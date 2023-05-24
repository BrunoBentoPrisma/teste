using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Database.Entidades
{
    public class ItensOrdemDeProducao : BaseEntity
    {
        [Column("ProdutoId")]
        [Required]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("OrdemDeProducaoId")]
        public Guid OrdemDeProducaoId { get; set; }
        [Column("SequenciaItem")]
        public int SequenciaItem { get; set; }
        [Column("QuantidadeUnidadeEstoque", TypeName = "decimal(24,4)")]
        public decimal QuantidadeUnidadeEstoque { get; set; }
        [Column("QuantidadeComprometidaLote", TypeName = "decimal(24,4)")]
        public decimal QuantidadeComprometidaLote { get; set; }
        [Column("GrupoId")]
        public Guid? GrupoId { get; set; }
    }
}