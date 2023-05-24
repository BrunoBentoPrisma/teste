using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class ConfigEstoqueFilial
    {
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("EmpresaId")]
        public Guid EmpresaId { get; set; }
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }
        [Column("EstoqueMinimo", TypeName = "decimal(24,4)")]
        public decimal EstoqueMinimo { get; set; }
        [Column("EstoqueMaximo", TypeName = "decimal(24,4)")]
        public decimal EstoqueMaximo { get; set; }
        [Column("Excluido")]
        public bool Excluido { get; set; }
        [Column("ControlaEstoqueMinimoMaximoPorFilial")]
        public bool ControlaEstoqueMinimoMaximoPorFilial { get; set; }
    }
}