using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class ItensFormulaVenda : BaseEntity
    {
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("ValorCusto", TypeName = "decimal(24,4)")]
        public decimal ValorCusto { get; set; }
        [Column("ValorUnitario", TypeName = "decimal(24,4)")]
        public decimal ValorUnitario { get; set; }
        [Column("ValorTotal", TypeName = "decimal(24,4)")]
        public decimal ValorTotal { get; set; }
        [Column("FormulaVendaId")]
        public Guid FormulaVendaId { get; set; }
        [Column("VendaId")]
        public Guid? VendaId { get; set; }

    }
}
