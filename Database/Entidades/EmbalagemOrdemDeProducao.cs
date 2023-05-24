using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class EmbalagemOrdemDeProducao : BaseEntity
    {
        [Column("OrdemDeProducaoId")]
        public Guid OrdemDeProducaoId { get; set; }
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("GrupoId")]
        public Guid GrupoId { get; set; }
        public Grupo Grupo { get; set; } = null!;
        [Column("AnoOrdemProducao")]
        public DateTime AnoOrdemProducao { get; set; }
        [Column("QuatidadeComprometidaLote", TypeName = "decimal(24,5)")]
        public decimal QuatidadeComprometidaLote { get; set; }
        [Column("QuatidadeUnidadeEstoque", TypeName = "decimal(24,5)")]
        public decimal QuatidadeUnidadeEstoque { get; set; }
        public List<ItensEmbalagemOrdemDeProducao> ItensEmbalagemOrdemDeProducao { get; set; } = new();
    }
}