using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class Rel_ConsumoProduto
    {
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }
        [Column("GrupoId")]
        public Guid GrupoId { get; set; }
        [Column("Comprar", TypeName = "decimal(24,5)")]
        public decimal Comprar { get; set; }
        [Column("Consumo", TypeName = "decimal(24,5)")]
        public decimal Consumo { get; set; }
        [Column("Curva")]
        public int? Curva { get; set; }
        [Column("Descricao")]
        [MaxLength(50)]
        public string Descricao { get; set; } = string.Empty;
        [Column("Encomenda")]
        public int? Encomenda { get; set; }
        [Column("Estoque", TypeName = "decimal(24,5)")]
        public decimal Estoque { get; set; }
        [Column("EstoqueTotal", TypeName = "decimal(24,5)")]
        public decimal EstoqueTotal { get; set; }
        [Column("OrdemDeProducaoId")]
        public Guid? OrdemDeProducaoId { get; set; }
        [Column("VendaId")]
        public Guid? VendaId { get; set; }
        [Column("TotalParaDias", TypeName = "decimal(24,5)")]
        public decimal TotalParaDias { get; set; }
        [Column("Unidade")]
        [MaxLength(5)]
        public string Unidade { get; set; } = string.Empty;
        [Column("EstoqueCobertura", TypeName = "decimal(24,5)")]
        public decimal EstoqueCobertura { get; set; }
    }
}
