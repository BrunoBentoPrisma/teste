using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Dtos.CompraFornecedor
{
    public class CompraFornecedorViewDto
    {
        public Guid Id { get; set; }
        public Guid CompraId { get; set; }
        public string? Observacao { get; set; }
        public StatusCotacao StatusCotacao { get; set; } = StatusCotacao.AEmitir;
        public string? FormaPagamento { get; set; }
        public string? Frete { get; set; }
        public DateTime? DataPreveEntrega { get; set; }
        public int StatusPedido { get; set; }
        public int NumeroNota { get; set; }
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public List<ItensCompraFornecedorViewDto> ItensCompraFornecedor { get; set; } = new List<ItensCompraFornecedorViewDto>();
    }
}
