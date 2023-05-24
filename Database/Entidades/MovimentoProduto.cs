using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ms_Compras.Enum;

namespace Ms_Compras.Database.Entidades
{
    public class MovimentoProduto : BaseEntity
    {
        [Column("ProdutoId")]
        [Required]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("GrupoId")]
        public Guid? GrupoId { get; set; }
        [Column("QuantidadeMovimentada", TypeName = "decimal(24,4)")]
        public decimal QuantidadeMovimentada { get; set; }
        [Column("Saldo", TypeName = "decimal(24,4)")]
        public decimal Saldo { get; set; }
        [Column("TipoMovimento")]
        public TipoMovimentoProduto TipoMovimentoProduto { get; set; }
        [Column("ItensOrdemDeProducaoId")]
        public Guid? ItensOrdemDeProducaoId { get; set; }
    }
}