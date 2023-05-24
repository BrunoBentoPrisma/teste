using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class ItensVenda : BaseEntity
    {
        [Column("ProdutoId")]
        [Required]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("Quantidade", TypeName = "decimal(24,4)")]
        [Required]
        public decimal Quantidade { get; set; }
        [Column("ValorUnitario", TypeName = "decimal(24,4)")]
        [Required]
        public decimal ValorUnitario { get; set; }
        [Column("Total", TypeName = "decimal(24,4)")]
        [Required]
        public decimal Total { get; set; }
        [Column("Custo", TypeName = "decimal(24,4)")]
        [Required]
        public decimal Custo { get; set; }
        [Column("VendaId")]
        public Guid VendaId { get; set; }
        [Column("ImpressaEtiquetaItem")]
        public int ImpressaEtiquetaItem { get; set; }
        [Column("PercentualAcrescimo", TypeName = "decimal(24,4)")]
        public decimal PercentualAcrescimo { get; set; }
        [Column("PercentualComissao", TypeName = "decimal(24,4)")]
        public decimal PercentualComissao { get; set; }
        [Column("PercentualDesconto", TypeName = "decimal(24,4)")]
        public decimal PercentualDesconto { get; set; }
        [Column("SequenciaItem")]
        public int SequenciaItem { get; set; }
        [Column("ValorAcrescimo", TypeName = "decimal(24,4)")]
        public decimal ValorAcrescimo { get; set; }
        [Column("ValorDesconto", TypeName = "decimal(24,4)")]
        public decimal ValorDesconto { get; set; }
        [Column("ValorLiquido", TypeName = "decimal(24,4)")]
        public decimal ValorLiquido { get; set; }
        [Column("ValorVenda", TypeName = "decimal(24,4)")]
        public decimal ValorVenda { get; set; }
        [Column("ValorCusto", TypeName = "decimal(24,4)")]
        public decimal ValorCusto { get; set; }
        [Column("GupoId")]
        public Guid GrupoId { get; set; }
    }
}