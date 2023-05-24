using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos.FaltasEncomendas
{
    public class FaltasEncomendasCreateDto
    {
        public Guid? ClienteId { get; set; }
        
        [Required(ErrorMessage = "É obrigatório informar um grupo.")]
        public Guid GrupoId { get; set; }
        
        [Required(ErrorMessage = "É obrigatório informar um produto.")]
        public Guid ProdutoId { get; set; }
        
        [Required(ErrorMessage = "É obrigatório informar um vendedor.")]
        public Guid VendedorId { get; set; }
        
        public Guid? CompraId { get; set; }
        public string? Observacao { get; set; }
        public DateTime? PrevisaoDeEntrega { get; set; }
        [Required(ErrorMessage = "Quantidade é obrigatório.")]
        public decimal Quantidade { get; set; }
        public int? Status { get; set; }
        public string? Telefone { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Tipo de encomenda inválido.")]
        public int Tipo { get; set; }
        [StringLength(3)]
        public string? Ddd { get; set; }
    }
}