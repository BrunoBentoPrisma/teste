using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Database.Entidades
{
    public class FaltasEncomendas : BaseEntity
    {
        [Column("ClienteId")]
        public Guid? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        [Column("GrupoId")]
        [Required(ErrorMessage = "É obrigatório informar um grupo.")]
        public Guid GrupoId { get; set; }
        public Grupo Grupo { get; set; } = null!;
        [Column("ProdutoId")]
        [Required(ErrorMessage = "É obrigatório informar um produto.")]
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        [Column("VendedorId")]
        [Required(ErrorMessage = "É obrigatório informar um vendedor.")]
        public Guid VendedorId { get; set; }
        public Vendedor Vendedor { get; set; } = null!;
        [Column("CompraId")]
        public Guid? CompraId { get; set; }
        [Column("Observacao")]
        [StringLength(1000)]
        public string? Observacao { get; set; }
        [Column("PrevisaoDeEntrega")]
        public DateTime? PrevisaoDeEntrega { get; set; }
        [Column("Quantidade", TypeName = "decimal(12,4)")]
        [Required(ErrorMessage = "Quantidade é obrigatório.")]
        public decimal Quantidade { get; set; }
        [Column("Status")]
        public int? Status { get; set; }
        [Column("Telefone")]
        [StringLength(20)]
        public string? Telefone { get; set; }
        [Column("Tipo")]
        [Required]
        [Range(0, 1, ErrorMessage = "Tipo de encomenda inválido.")]
        public int Tipo { get; set; }
        [Column("Ddd")]
        [MaxLength(3)]
        public string? Ddd { get; set; }
    }
}
