using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class ItensCompra : BaseEntity
    {
        [Column("LaboratorioId")]
        public Guid? LaboratorioId { get; set; }
        public Laboratorio? Laboratorio { get; set; }
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("Comprar")]
        public bool Comprar { get; set; }
        [Column("Encomenda")]
        public bool Encomenda { get; set; }
        [Column("Estoque", TypeName = "decimal(24,4)")]
        public decimal Estoque { get; set; }
        [Column("CompraId")]
        public Guid CompraId { get; set; }
        [Column("QuantidadeCompra", TypeName = "decimal(24,4)")]
        public decimal QuantidadeCompra { get; set; }
        [Column("QuantidadeVendida", TypeName = "decimal(24,4)")]
        public decimal QuantidadeVendida { get; set; }
        [Column("QuantidadeTotal", TypeName = "decimal(24,4)")]
        public decimal QuantidadeTotal { get; set; }
        [Column("ValorTotal", TypeName = "decimal(24,4)")]
        public decimal ValorTotal { get; set; }
        [Column("ValorUnitario", TypeName = "decimal(24,4)")]
        public decimal ValorUnitario { get; set; }
        [Column("ValorVendido", TypeName = "decimal(24,4)")]
        public decimal ValorVendido { get; set; }
        [Column("SelecionadoGerar")]
        [StringLength(10)]
        public string? SelecionadoGerar { get; set; }
        [Column("ConsumoDiario", TypeName = "decimal(24,4)")]
        public decimal ConsumoDiario { get; set; }
        [Column("QuantidadeCompraMaxima", TypeName = "decimal(24,4)")]
        public decimal QuantidadeCompraMaxima { get; set; }
        [Column("Ddd")]
        [StringLength(2)]
        public string? Ddd { get; set; }
    }
}
