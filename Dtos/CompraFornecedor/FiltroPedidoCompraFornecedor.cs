using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos.CompraFornecedor
{
    public class FiltroPedidoCompraFornecedor
    {
        public Guid? FornecedorId { get; set; }
        public Guid? GrupoId { get; set; }
        public Guid? ProdutoId { get; set; }
        [Required]
        public DateTime DataInicial { get; set; }
        [Required]
        public DateTime DataFinal { get; set; }
        public Guid? CompraId { get; set; }
        public Guid? CompraFornecedorId { get; set; }
        public int? StatusPedido { get; set; }
    }
}