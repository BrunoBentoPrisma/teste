using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class Venda : BaseEntity
    {
        [Column("Status")]
        public int Status { get; set; }
        [Column("DataDeEmissao")]
        public DateTime DataDeEmissao { get; set; }
        [Column("Orcamento")]
        public int Orcamento { get; set; }
        [Column("Total", TypeName = "decimal(24,4)")]
        public decimal Total { get; set; }
        [Column("NumeroVenda", TypeName = "decimal(24,4)")]
        public decimal NumeroVenda { get; set; }
        public List<ItensVenda> ItensVenda { get; set; } = new();
    }
}