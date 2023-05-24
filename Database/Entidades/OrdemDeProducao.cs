using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Database.Entidades
{
    public class OrdemDeProducao : BaseEntity
    {
        [Column("ClienteId")]
        [Required]
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        [Column("Status")]
        public int Status { get; set; }
        [Column("AnoOrdemDeProducao")]
        public DateTime AnoOrdemDeProducao { get; set; }
        [Column("DataDeEmissao")]
        public DateTime DataDeEmissao { get; set; }
        [Column("CodigoForma", TypeName = "decimal(24,4)")]
        public decimal CodigoForma { get; set; }
        [Column("NumeroOrdemDeProducao")]
        public string NumeroOrdemDeProducao { get; set; } = string.Empty;
        [Column("ProdutoCapsulaId")]
        public Guid? ProdutoCapsulaId { get; set; }
        [Column("ProdutoEmbalagemId")]
        public Guid ProdutoEmbalagemId { get; set; }
        [Column("GrupoCapsulaId")]
        public Guid? GrupoCapsulaId { get; set; }
        [Column("GrupoEmbalagemId")]
        public Guid GrupoEmbalagemId { get; set; }
        [Column("CapsulaOrdemDeProducao")]
        public int CapsulaOrdemDeProducao { get; set; }
        [Column("VendaId")]
        public Guid? VendaId { get; set; }
        public Venda? Venda { get; set; }
        [Column("FormulaVendaId")]
        public Guid? FormulaVendaId { get; set; }
        [Column("ItensFormulaVendaId")]
        public Guid? ItensFormulaVendaId { get; set; }
        public List<ItensOrdemDeProducao> ItensOrdemDeProducao { get; set; } = null!;
    }
}