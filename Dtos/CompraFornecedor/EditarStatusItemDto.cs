using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos.CompraFornecedor
{
    public class EditarStatusItemDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int StatusItem { get; set; }
    }
}