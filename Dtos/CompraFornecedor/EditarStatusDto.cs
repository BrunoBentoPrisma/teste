using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos.CompraFornecedor
{
    public class EditarStatusDto
    {
        [Required]
        public Guid Id { get; set; }
        public DateTime? DataPreveEntrega { get; set; }
        public int? StatusPedido { get; set; }
    }
}