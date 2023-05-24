using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class CompraFornecedor : BaseEntity
    {
        [Column("CompraId")]
        public Guid CompraId { get; set; }
        [Column("Observacao")]
        [StringLength(30000)]
        public string? Observacao { get; set; }
        [Column("StatusCotacao")]
        public StatusCotacao StatusCotacao { get; set; } = StatusCotacao.AEmitir;
        [Column("FormaPagamento")]
        [StringLength(100)]
        public string? FormaPagamento { get; set; }
        [Column("Frete")]
        [StringLength(100)]
        public string? Frete { get; set; }
        [Column("DataPreveEntrega")]
        public DateTime? DataPreveEntrega { get; set; }
        [Column("StatusPedido")]
        public int StatusPedido { get; set; }
        [Column("NumeroNota")]
        public int NumeroNota { get; set; }
        [Column("FornecedorId")]
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public List<ItensCompraFornecedor> ItensCompraFornecedor { get; set; } = new List<ItensCompraFornecedor>();
    }
    public enum StatusCotacao
    {
        AEmitir,
        Rejeitada,
        Emitida,
        Processada
    }
}
