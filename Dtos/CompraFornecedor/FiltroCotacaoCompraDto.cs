using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos.CompraFornecedor
{
    public class FiltroCotacaoCompraDto
    {
        [Required]
        public Guid CompraId { get; set; }
        [Required]
        public DateTime DataFinal { get; set; }
        [Required]
        public DateTime DataInicial { get; set; }
    }
}