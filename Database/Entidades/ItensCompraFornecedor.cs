using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class ItensCompraFornecedor : BaseEntity
    {
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("Comprar")]
        public bool Comprar { get; set; }
        [Column("DataValidade")]
        public DateTime DataValidade { get; set; }
        [Column("CompraFornecedorId")]
        public Guid CompraFornecedorId { get; set; }
        [Column("QuantidadeCompra", TypeName = "decimal(24,4)")]
        public decimal QuantidadeCompra { get; set; }
        [Column("ValorTotal", TypeName = "decimal(24,4)")]
        public decimal ValorTotal { get; set; }
        [Column("ValorUnitario", TypeName = "decimal(24,4)")]
        public decimal ValorUnitario { get; set; }
        [Column("QuatidadeCompraUnidadeEstoque", TypeName = "decimal(24,4)")]
        public decimal QuatidadeCompraUnidadeEstoque { get; set; }
        [Column("ValorUnitarioUnidadeEstoque", TypeName = "decimal(24,4)")]
        public decimal ValorUnitarioUnidadeEstoque { get; set; }
        [Column("StatusItemPedido")]
        public int StatusItemPedido { get; set; }
        [Column("SelecionadoGerar")]
        [StringLength(10)]
        public string SelecionadoGerar { get; set; } = string.Empty;
    }
}
