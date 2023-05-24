using Ms_Compras.Database.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos.CompraFornecedor
{
    public class CompraFornecedorCreateDto
    {
        [Required]
        public Guid CompraId { get; set; }
        public string? Observacao { get; set; }
        public StatusCotacao StatusCotacao { get; set; } = StatusCotacao.AEmitir;
        public string? FormaPagamento { get; set; }
        public string? Frete { get; set; }
        public DateTime? DataPreveEntrega { get; set; }
        public int StatusPedido { get; set; }
        public int NumeroNota { get; set; }
        [Required]
        public Guid FornecedorId { get; set; }
        public List<ItensCompraFornecedorCreateDto> ItensCompraFornecedor { get; set; } = new List<ItensCompraFornecedorCreateDto>();
    }
}
